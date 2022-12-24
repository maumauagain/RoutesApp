using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EvaluatePosts
{
    public static class EvaluatePost
    {
        [FunctionName("EvaluatePost")]
        public static async void Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            string cs = Environment.GetEnvironmentVariable("MyConnectionString", EnvironmentVariableTarget.Process);

            var command1 = PublishPost();
            var command2 = RejectPost();
            string[] commands = { command1, command2 };

            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();

                foreach (var command in commands)
                {
                    using (SqlCommand cmd = new SqlCommand(command, conn))
                    {
                        // Execute the command and log the # rows affected.
                        var rows = await cmd.ExecuteNonQueryAsync();
                        log.LogInformation($"{rows} rows were updated");
                    }
                }
            }

        }

        public static string PublishPost()
        {
            return
 @"UPDATE p
SET IsPublished = 1
	,UpdatedOn = GETDATE()
FROM posts p
INNER JOIN (
	SELECT p.Id
		,count(pl.id) AS QTDLike
		,count(pd.id) AS QTDDislike
	FROM posts p
	LEFT JOIN PostLikes pl ON p.Id = pl.PostId
	LEFT JOIN PostDislikes pd ON p.Id = pd.PostId
	WHERE p.CreatedOn <= DATEADD(day, - 1, GETDATE())
		AND IsActive = 1
		AND IsPublished = 0
		AND IsRejected = 0
	GROUP BY p.Id
		,pl.Id
		,pd.Id
	HAVING (
			count(pd.id) = 0
			AND count(pl.id) > 0
			)
		OR (
			count(pd.id) > 0
			AND count(pl.id) > 0
			AND count(pl.id) / count(pd.id) >= 1.7
			)
	) p2 ON p2.Id = p.Id";

        }

        public static string RejectPost()
        {
            return
 @"UPDATE p
SET IsRejected = 1
	,UpdatedOn = GETDATE()
FROM posts p
INNER JOIN (
	SELECT p.Id
		,count(pl.id) AS QTDLike
		,count(pd.id) AS QTDDislike
	FROM posts p
	LEFT JOIN PostLikes pl ON p.Id = pl.PostId
	LEFT JOIN PostDislikes pd ON p.Id = pd.PostId
	WHERE p.CreatedOn <= DATEADD(day, - 1, GETDATE())
		AND IsActive = 1
		AND IsPublished = 0
		AND IsRejected = 0
	GROUP BY p.Id
		,pl.Id
		,pd.Id
	HAVING (
			count(pd.id) = 0
			AND count(pl.id) = 0
			)
		OR (
			count(pd.id) > count(pl.id)
			OR (
				COUNT(pl.id) > 0
				AND count(pd.id) > 0
				AND (count(pl.id) / count(pd.id) < 1.7)
				)
			)
	) p2 ON p2.Id = p.Id";
        }
    }
}
