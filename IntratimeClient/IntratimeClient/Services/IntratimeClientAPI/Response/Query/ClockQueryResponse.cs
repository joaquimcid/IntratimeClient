using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IntratimeClient.Services.IntratimeClientAPI.Response.Query
{
    public partial class ClockQueryResponse
    {
        [JsonProperty("INOUT_ID")]
        public long InoutId { get; set; }

        [JsonProperty("INOUT_USER_ID")]
        public long InoutUserId { get; set; }

        [JsonProperty("INOUT_TYPE")]
        public long InoutType { get; set; }

        [JsonProperty("INOUT_DATE")]
        public DateTimeOffset InoutDate { get; set; }

        [JsonProperty("INOUT_END_DATE")]
        public DateTimeOffset? InoutEndDate { get; set; }

        [JsonProperty("INOUT_DEVICE_DATE")]
        public DateTimeOffset? InoutDeviceDate { get; set; }

        [JsonProperty("INOUT_SOURCE")]
        public long InoutSource { get; set; }

        [JsonProperty("INOUT_COORDINATES")]
        public string InoutCoordinates { get; set; }

        [JsonProperty("INOUT_USE_SERVER_TIME")]
        public long InoutUseServerTime { get; set; }

        [JsonProperty("INOUT_PROJECT_ID")]
        public object InoutProjectId { get; set; }

        [JsonProperty("INOUT_MODIFICATION_DATE")]
        public DateTimeOffset? InoutModificationDate { get; set; }

        [JsonProperty("INOUT_COMMENTS")]
        public string InoutComments { get; set; }

        [JsonProperty("INOUT_ADMIN_COMMENTS")]
        public object InoutAdminComments { get; set; }

        [JsonProperty("INOUT_IMAGE_NAME")]
        public object InoutImageName { get; set; }

        [JsonProperty("INOUT_CREATETIME")]
        public DateTimeOffset? InoutCreatetime { get; set; }

        [JsonProperty("INOUT_DELETED_AT")]
        public object InoutDeletedAt { get; set; }

        [JsonProperty("INOUT_EXPENSE_ID")]
        public long? InoutExpenseId { get; set; }

        [JsonProperty("INOUT_REQUEST_TYPE_ID")]
        public long? InoutRequestTypeId { get; set; }

        [JsonProperty("INOUT_ABSENCE_IS_JUSTIFIED")]
        public long InoutAbsenceIsJustified { get; set; }

        [JsonProperty("INOUT_AMOUNT")]
        public string InoutAmount { get; set; }

        [JsonProperty("INOUT_DEVICE_UID")]
        public string InoutDeviceUid { get; set; }

        [JsonProperty("INOUT_PROJECT_NAME")]
        public object InoutProjectName { get; set; }

        [JsonProperty("INOUT_EXPENSE_NAME")]
        public string InoutExpenseName { get; set; }

        [JsonProperty("INOUT_IMAGE_URL")]
        public object InoutImageUrl { get; set; }
    }

    public partial class ClockQueryResponse
    {
        public static ClockQueryResponse[] FromJson(string json) => JsonConvert.DeserializeObject<ClockQueryResponse[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ClockQueryResponse[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
