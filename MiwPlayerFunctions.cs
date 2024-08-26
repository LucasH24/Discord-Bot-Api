using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MiwBotApi.Models;
using System.Linq.Expressions;

namespace MiwBotApi
{
    internal class MiwPlayerFunctions
    {

        [FunctionName("GetStatsByUUID")]
        public static async Task<IActionResult> GetStatsByUUID(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "miwPlayers/uuid/{uuid}")] HttpRequest req, ILogger log, string uuid)
        {
            List<MiwPlayer> playersList = new List<MiwPlayer>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    var query = @"Select * from MiwPlayers Where UUID = @UUID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UUID", uuid);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        MiwPlayer miwPlayer = new MiwPlayer()
                        {
                            MiwPlayerID = (int)reader["MiwPlayerID"],
                            UUID = reader["UUID"].ToString(),
                            Username = reader["Username"].ToString(),

                            Wins = (int)reader["Wins"],
                            Kills = (int)reader["Kills"],
                            Finals = (int)reader["Finals"],
                            WitherDamage = (int)reader["WitherDamage"],
                            WitherKills = (int)reader["WitherKills"],
                            Deaths = (int)reader["Deaths"],
                            ArrowsHit = (int)reader["ArrowsHit"],
                            ArrowsShot = (int)reader["ArrowsShot"],

                            KD = (decimal)reader["KD"],
                            FKD = (decimal)reader["FKD"],
                            TKD = (decimal)reader["TKD"],
                            WDD = (decimal)reader["WDD"],
                            WKD = (decimal)reader["WKD"],
                            AA = (decimal)reader["AA"],
                            RATE = (decimal)reader["RATE"],

                            KPW = (decimal)reader["KPW"],
                            FPW = (decimal)reader["FPW"],
                            TKPW = (decimal)reader["TKPW"],
                            WDPW = (decimal)reader["WDPW"],
                            WKPW = (decimal)reader["WKPW"],
                            DPW = (decimal)reader["DPW"],
                            SPW = (decimal)reader["SPW"],

                            RankWins = (int)reader["RankWins"],
                            RankKills = (int)reader["RankKills"],
                            RankFinals = (int)reader["RankFinals"],
                            RankWitherDamage = (int)reader["RankWitherDamage"],
                            RankWitherKills = (int)reader["RankWitherKills"],
                            RankDeaths = (int)reader["RankDeaths"],
                            RankArrowsHit = (int)reader["RankArrowsHit"],
                            RankArrowsShot = (int)reader["RankArrowsShot"],

                            RankKD = (int)reader["RankKD"],
                            RankFKD = (int)reader["RankFKD"],
                            RankTKD = (int)reader["RankTKD"],
                            RankWDD = (int)reader["RankWDD"],
                            RankWKD = (int)reader["RankWKD"],
                            RankAA = (int)reader["RankAA"],
                            RankRATE = (int)reader["RankRATE"],

                            RankKPW = (int)reader["RankKPW"],
                            RankFPW = (int)reader["RankFPW"],
                            RankTKPW = (int)reader["RankTKPW"],
                            RankWDPW = (int)reader["RankWDPW"],
                            RankWKPW = (int)reader["RankWKPW"],
                            RankDPW = (int)reader["RankDPW"],
                            RankSPW = (int)reader["RankSPW"],
                        };
                        playersList.Add(miwPlayer);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (playersList.Count > 0)
            {
                return new OkObjectResult(playersList);
            }
            else
            {
                return new NotFoundResult();
            }
        }


        [FunctionName("GetLeaderboard")]
        public static async Task<IActionResult> GetLeaderboard(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "miwPlayers/leaderboard/{type}")] HttpRequest req, ILogger log, string type)
        {
            List<MiwPlayer> playersList = new List<MiwPlayer>();
            try
            {
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))
                {
                    connection.Open();
                    /*
                    string lbType = "";
                    string[] listOfLbs = ["Wins", "Kills", "Finals", "WitherDamage", "WitherKills", "Deaths", "ArrowsHit", "ArrowsShot", "KD", "FKD", "TKD", "WDD", "WKD", "AA", "RATE", "KPW", "FPW", "TKPW", "WDPW", "WKPW", "DPW", "SPW"];
                    foreach (string x in listOfLbs)
                    {
                        if (x == type)
                        {
                            lbType = x;
                        }
                    }
                    var query = @"Select TOP 25 * from MiwPlayers ORDER BY @lbType DESC";
                    */

                    //dont understand why you cant order by a variable even when preventing sql injection
                    // this is the best fix I could find without making 20 different leaderboard functions
                    // its still gross
                    var query = "";
                    switch (type)
                    {
                        case "Wins":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY Wins DESC"; break;
                        case "Kills":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY Kills DESC"; break;
                        case "Finals":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY Finals DESC"; break;
                        case "WitherDamage":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY WitherDamage DESC"; break;
                        case "WitherKills":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY WitherKills DESC"; break;
                        case "Deaths":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY Deaths DESC"; break;
                        case "ArrowsShot":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY ArrowsShot DESC"; break;
                        case "ArrowsHit":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY ArrowsHit DESC"; break;

                        case "KD":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY KD DESC"; break;
                        case "FKD":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY FKD DESC"; break;
                        case "TKD":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY TKD DESC"; break;
                        case "WDD":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY WDD DESC"; break;
                        case "WKD":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY WKD DESC"; break;
                        case "AA":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY AA DESC"; break;
                        case "RATE":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY RATE DESC"; break;

                        case "KPW":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY KPW DESC"; break;
                        case "FPW":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY FPW DESC"; break;
                        case "TKPW":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY TKPW DESC"; break;
                        case "WDPW":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY WDPW DESC"; break;
                        case "WKPW":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY WKPW DESC"; break;
                        case "DPW":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY DPW DESC"; break;
                        case "SPW":
                            query = @"Select TOP 25 * from MiwPlayers ORDER BY SPW DESC"; break;
                        default:
                            break;
                    }
                    SqlCommand command = new SqlCommand(query, connection);
                    var reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        MiwPlayer miwPlayer = new MiwPlayer()
                        {
                            MiwPlayerID = (int)reader["MiwPlayerID"],
                            UUID = reader["UUID"].ToString(),
                            Username = reader["Username"].ToString(),

                            Wins = (int)reader["Wins"],
                            Kills = (int)reader["Kills"],
                            Finals = (int)reader["Finals"],
                            WitherDamage = (int)reader["WitherDamage"],
                            WitherKills = (int)reader["WitherKills"],
                            Deaths = (int)reader["Deaths"],
                            ArrowsHit = (int)reader["ArrowsHit"],
                            ArrowsShot = (int)reader["ArrowsShot"],

                            KD = (decimal)reader["KD"],
                            FKD = (decimal)reader["FKD"],
                            TKD = (decimal)reader["TKD"],
                            WDD = (decimal)reader["WDD"],
                            WKD = (decimal)reader["WKD"],
                            AA = (decimal)reader["AA"],
                            RATE = (decimal)reader["RATE"],

                            KPW = (decimal)reader["KPW"],
                            FPW = (decimal)reader["FPW"],
                            TKPW = (decimal)reader["TKPW"],
                            WDPW = (decimal)reader["WDPW"],
                            WKPW = (decimal)reader["WKPW"],
                            DPW = (decimal)reader["DPW"],
                            SPW = (decimal)reader["SPW"],

                            RankWins = (int)reader["RankWins"],
                            RankKills = (int)reader["RankKills"],
                            RankFinals = (int)reader["RankFinals"],
                            RankWitherDamage = (int)reader["RankWitherDamage"],
                            RankWitherKills = (int)reader["RankWitherKills"],
                            RankDeaths = (int)reader["RankDeaths"],
                            RankArrowsHit = (int)reader["RankArrowsHit"],
                            RankArrowsShot = (int)reader["RankArrowsShot"],

                            RankKD = (int)reader["RankKD"],
                            RankFKD = (int)reader["RankFKD"],
                            RankTKD = (int)reader["RankTKD"],
                            RankWDD = (int)reader["RankWDD"],
                            RankWKD = (int)reader["RankWKD"],
                            RankAA = (int)reader["RankAA"],
                            RankRATE = (int)reader["RankRATE"],

                            RankKPW = (int)reader["RankKPW"],
                            RankFPW = (int)reader["RankFPW"],
                            RankTKPW = (int)reader["RankTKPW"],
                            RankWDPW = (int)reader["RankWDPW"],
                            RankWKPW = (int)reader["RankWKPW"],
                            RankDPW = (int)reader["RankDPW"],
                            RankSPW = (int)reader["RankSPW"],
                        };
                        playersList.Add(miwPlayer);
                    }
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            if (playersList.Count > 0)
            {
                return new OkObjectResult(playersList);
            }
            else
            {
                return new NotFoundResult();
            }
        }

    }
}
