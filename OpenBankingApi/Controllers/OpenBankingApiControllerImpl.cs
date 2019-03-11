using System;
using System.Threading.Tasks;

namespace OpenBankingApi.Controllers
{
    public class OpenBankingApiControllerImpl: IController
    {
        public Task<OBReadAccount3> GetAccountsAsync(string x_fapi_financial_id, string authorization, string x_fapi_customer_last_logged_time = null,
            string x_fapi_customer_ip_address = null, string x_fapi_interaction_id = null, string x_customer_user_agent = null)
        {
            throw new NotImplementedException();
        }

        public Task<OBReadAccount3> GetAccountsAccountIdAsync(string accountId, string x_fapi_financial_id, string authorization,
            string x_fapi_customer_last_logged_time = null, string x_fapi_customer_ip_address = null,
            string x_fapi_interaction_id = null, string x_customer_user_agent = null)
        {
            throw new NotImplementedException();
        }

        public Task<OBReadBalance1> GetAccountsAccountIdBalancesAsync(string accountId, string x_fapi_financial_id, string authorization,
            string x_fapi_customer_last_logged_time = null, string x_fapi_customer_ip_address = null,
            string x_fapi_interaction_id = null, string x_customer_user_agent = null)
        {
            throw new NotImplementedException();
        }

        public Task<OBReadStatement1> GetAccountsAccountIdStatementsAsync(string accountId, string x_fapi_financial_id, string authorization,
            string x_fapi_customer_last_logged_time = null, string x_fapi_customer_ip_address = null,
            string x_fapi_interaction_id = null, DateTimeOffset? fromStatementDateTime = null,
            DateTimeOffset? toStatementDateTime = null, string x_customer_user_agent = null)
        {
            throw new NotImplementedException();
        }

        public Task<OBReadStatement1> GetAccountsAccountIdStatementsStatementIdAsync(string statementId, string accountId, string x_fapi_financial_id,
            string authorization, string x_fapi_customer_last_logged_time = null, string x_fapi_customer_ip_address = null,
            string x_fapi_interaction_id = null, string x_customer_user_agent = null)
        {
            throw new NotImplementedException();
        }

        public Task<OBReadTransaction4> GetAccountsAccountIdTransactionsAsync(string accountId, string x_fapi_financial_id, string authorization,
            string x_fapi_customer_last_logged_time = null, string x_fapi_customer_ip_address = null,
            string x_fapi_interaction_id = null, DateTimeOffset? fromBookingDateTime = null,
            DateTimeOffset? toBookingDateTime = null, string x_customer_user_agent = null)
        {
            return new Task<OBReadTransaction4>(() =>
            {
                return new OBReadTransaction4();

            });
        }
    }
}