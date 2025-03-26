using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace CHLeMudra.Data
{
    public class CommonHelper
    {
        public static Dictionary<string, string> Getrating()
        {
            Dictionary<string, string> rating = new Dictionary<string, string>();

            rating.Add("Good", "Good");
            rating.Add("Satisfactory", "Satisfactory");
            rating.Add("Dissatisfied", "Dissatisfied");
            return rating;
        }

        public static Dictionary<int, string> getYears()
        {
            Dictionary<int, string> Years = new Dictionary<int, string>();
            for (int i = 2018; i <= 2025; i++)
            {
                Years.Add(i, i.ToString());
            }
            return Years;
        }

        public static Dictionary<int, int> getHours()
        {
            Dictionary<int, int> hours = new Dictionary<int, int>();
            for (int i = 0; i <= 23; i++)
            {
                hours.Add(i, i);
            }
            return hours;
        }
        public static string getTimeFormat(TimeSpan tim)
        {
            try
            {
                DateTime d = DateTime.Parse(tim.ToString());
                return d.ToString("hh:mm tt");
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

                return "";
            }

        }
        public static List<DateTime> GetAllDatesOfMonth(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }
        public static Dictionary<int, int> getMinute()
        {
            Dictionary<int, int> minutes = new Dictionary<int, int>();
            for (int i = 0; i <= 59; i++)
            {
                minutes.Add(i, i);
            }
            return minutes;
        }
        public static double CheckHoursDiffrence(DateTime fromdate)
        {
            TimeSpan elapsedTime = fromdate - DateTime.Now;

            return elapsedTime.TotalHours;
            //return (elapsedTime.TotalSeconds > -1 && elapsedTime.TotalSeconds < 0) 
            //       || (elapsedTime.TotalSeconds >= 0 && Math.Floor(elapsedTime.TotalSeconds) <= aliveDurationMinutes * 60); 
        }
        //public static bool SaveImage(string base64Str, string ImgName)
        //{
        //    String path = HttpContext.Current.Server.MapPath("~/Images/UploadImg/"); //Path

        //    //Check if directory exist
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
        //    }

        //    //set the image path
        //    string imgPath = Path.Combine(path, ImgName);

        //    byte[] imageBytes = Convert.FromBase64String(base64Str);

        //    File.WriteAllBytes(imgPath, imageBytes);

        //    return true;
        //}
        public static Dictionary<int, string> getMonth()
        {
            Dictionary<int, string> Months = new Dictionary<int, string>();

            Months.Add(1, "Jan");
            Months.Add(2, "Feb");
            Months.Add(3, "March");
            Months.Add(4, "April");
            Months.Add(5, "May");
            Months.Add(6, "June");
            Months.Add(7, "July");
            Months.Add(8, "Aug");
            Months.Add(9, "Sep");
            Months.Add(10, "Oct");
            Months.Add(11, "Nov");
            Months.Add(12, "Dec");
            return Months;

        }
        public static Dictionary<int, int> getDay()
        {
            Dictionary<int, int> y = new Dictionary<int, int>();

            for (int i = 1; i <= 31; i++)
            {
                y.Add(i, i);
            }
            return y;
        }

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = 2147483647)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }
        /// <summary>
        /// Ensure that a string is not null
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        /// <summary>
        /// Ensure that a string doesn't exceed maximum allowed length
        /// </summary>
        /// <param name="str">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="postfix">A string to add to the end if the original string was shorten</param>
        /// <returns>Input string if its lengh is OK; otherwise, truncated input string</returns>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }
            else
            {
                return str;
            }
        }

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static bool IsInteger(String s)
        {
            try
            {
                Int64.Parse(s);
            }
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
            {
                return false;
            }
            // only got here if we didn't return false
            return true;
        }
        public static int getAge(DateTime dob)
        {
            var datenow = System.DateTime.Now;
            var yearNow = datenow.Year;
            var monthNow = datenow.Month;
            var dayNow = datenow.Day;
            //var dob = new DateTime(model.DobYear.Value, model.Dobmonth.Value, model.DobDay.Value);
            var yearDob = dob.Year;
            var monthDob = dob.Month;
            var dayDob = dob.Day;
            int yearAge = yearNow - yearDob;
            int monthAge = 0;
            int dayAge = 0;

            if (monthNow >= monthDob)
                monthAge = monthNow - monthDob;
            else
            {
                yearAge--;
                monthAge = 12 + monthNow - monthDob;
            }

            if (dayNow >= dayDob)
                dayAge = dayNow - dayDob;
            else
            {
                monthAge--;
                var dateAge = 31 + dayNow - dayDob;

                if (monthAge < 0)
                {
                    monthAge = 11;
                    yearAge--;
                }
            }
            return yearAge;
        }

        static public string EncodeTo64(string toEncode)

        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }



        static public string DecodeFrom64(string encodedData)
        {

            byte[] encodedDataAsBytes

                = System.Convert.FromBase64String(encodedData);

            string returnValue =

               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            return returnValue;

        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }

}