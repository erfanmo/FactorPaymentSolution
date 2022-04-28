using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPayPropertyLib;

namespace FPayDataAccess
{
    public class CustomerOperation
    {

        public static DataTable GetCustomerByParameters(CustomerDB customer)
        {
            DataTable table = null;
            table = FPayDataSetTableAdapter.GetCustomer().GetCustomer(customer.CUSTOMER_CODE,
                customer.CUSTOMER_REGISTER_DATE,
                customer.CUSTOMER_SESSION,
                customer.CUSTOMER_MOBILE,
                customer.CUSTOMER_EMAIL,
                customer.CUSTOMER_FAMILY,
                customer.BUSINESS_TYPE_CODE,
                customer.CUSTOMER_BUSINESS_NAME,
                customer.CITY_CODE,
                customer.CUSTOMER_BIRTH_DATE);
            return (table);
        }

        public static void InsertCustomer(CustomerDB customer)
        {
            FPayDataSetTableAdapter.GetCustomer().InsertCustomer(long.Parse(customer.CUSTOMER_CODE),
                customer.CUSTOMER_REGISTER_DATE,
                customer.CUSTOMER_MOBILE,
                customer.CUSTOMER_EMAIL,
                customer.SEXTUALITY_CODE,
                customer.CUSTOMER_NAME,
                customer.CUSTOMER_FAMILY,
                customer.BUSINESS_TYPE_CODE,
                customer.CUSTOMER_BUSINESS_NAME,
                customer.CUSTOMER_TELL,
                customer.CITY_CODE,
                customer.CUSTOMER_ADDRESS,
                customer.CUSTOMER_BIRTH_DATE,
                customer.CUSTOMER_KEY,
                customer.CUSTOMER_SESSION,
                customer.CUSTOMER_PASSWORD,
                customer.ENABLED_STATE);
        }

        public static void UpdateCustomer(CustomerDB customer)
        {
            FPayDataSetTableAdapter.GetCustomer().UpdateCustomer(customer.CUSTOMER_CODE,
                customer.SEXTUALITY_CODE,
                customer.CUSTOMER_NAME,
                customer.CUSTOMER_FAMILY,
                customer.BUSINESS_TYPE_CODE,
                customer.CUSTOMER_BUSINESS_NAME,
                customer.CUSTOMER_TELL,
                customer.CITY_CODE,
                customer.CUSTOMER_ADDRESS,
                customer.CUSTOMER_BIRTH_DATE,
                customer.CUSTOMER_KEY,
                customer.CUSTOMER_SESSION,
                customer.CUSTOMER_PASSWORD,
                customer.ENABLED_STATE);
        }

    }
}
