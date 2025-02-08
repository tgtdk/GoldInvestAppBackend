using GoldInvestApp.Models;
using GoldInvestApp.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldInvestApp.Repository
{
    public class GoldInvestRepository : IGoldInvest
    {
        public readonly IConfiguration _configuration;
        public readonly ILogger<GoldInvestRepository> _logger;
        public readonly MySqlConnection _mySqlConnection;

        public GoldInvestRepository(IConfiguration configuration, ILogger<GoldInvestRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _mySqlConnection = new MySqlConnection(_configuration[key: "ConnectionStrings:MySqlDBString"]);
        }

        public async Task<GoldInvestModelResponse> getGoldPrice(GoldInvestModelRequest request)
        {
            GoldInvestModelResponse response = new GoldInvestModelResponse();
            response.IsSuccess = true;
            response.Message = "Successfully Get";

            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                //SqlQueries.ReadInformationById
                //using (MySqlCommand sqlCommand = new MySqlCommand("select" + request.cYear + "from gold_invest_table limit 1", _mySqlConnection))
                using (MySqlCommand sqlCommand = new MySqlCommand("select cPrice from gold_invest_table where cYear = " + request.cYear, _mySqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = System.Data.CommandType.Text;
                        sqlCommand.CommandTimeout = 180;
                        sqlCommand.Parameters.AddWithValue(parameterName: "@cYear", request.cYear);

                        using (MySqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                response.cPrice = "";

                                while (await dataReader.ReadAsync())
                                {
                                    //response.cPrice = dataReader[name: "cPrice"] != DBNull.Value ? Convert.ToString(dataReader[name: "cPrice"]) : string.Empty;
                                    response.cPrice = Convert.ToString(dataReader[name: "cPrice"]) != string.Empty ? Convert.ToString(dataReader[name: "cPrice"]) : string.Empty;
                                }
                            }
                            else
                            {
                                response.IsSuccess = true;
                                response.Message = "Record Not Foud / Database Empty";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Message = ex.Message;
                        _logger.LogError("ReadInformationById Error Occur : Message" + ex.Message);
                    }
                    finally
                    {
                        await sqlCommand.DisposeAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                _logger.LogError("ReadInformationById Error Occur : Message" + ex.Message);
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }
            return response;
        }
    }
}
