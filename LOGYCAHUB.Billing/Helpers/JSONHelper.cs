namespace LOGYCAHUB.Billing.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.Script.Serialization;

    public static class JSONHelper
    {
        #region Public extension methods.

        /// <summary>
        /// Extened method of object class, Converts an object to a json string.
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>serializer</returns>
        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        #endregion
    }
}