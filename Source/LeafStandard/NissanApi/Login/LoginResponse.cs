using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLeaf.NissanApi.Login
{
    public class VehicleInfo
    {
        [JsonProperty("charger20066")]
        public bool Charger { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("telematicsEnabled")]
        public bool TelematicsEnabled { get; set; }

        [JsonProperty("vin")]
        public string Vin { get; set; }

        [JsonProperty("custom_sessionid")]
        public string SessionId { get; set; }
    }

    public class VehicleProfile
    {
        [JsonProperty("vin")]
        public string Vin { get; set; }

        [JsonProperty("gdcUserId")]
        public string UserId { get; set; }

        [JsonProperty("gdcPassword")]
        public string Password { get; set; }

        [JsonProperty("encAuthToken")]
        public string EncodedAuthToken { get; set; }

        [JsonProperty("dcmId")]
        public string DcmId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusDate")]
        public DateTime? statusDate { get; set; }
    }

    public class Vehicle
    {
        [JsonProperty("profile")]
        public VehicleProfile Profile { get; set; }
    }

    public class CustomerVehicleInfo
    {
        [JsonProperty("VIN")]
        public string Vin { get; set; }

        [JsonProperty("DCMID")]
        public string DcmId { get; set; }

        [JsonProperty("SIMID")]
        public string SimId { get; set; }

        [JsonProperty("NAVIID")]
        public string NaviId { get; set; }

        [JsonProperty("EncryptedNAVIID")]
        public string EncryptedNaviId { get; set; }

        [JsonProperty("MSN")]
        public string Msn { get; set; }

        public DateTime? LastVehicleLoginTime { get; set; }
        public DateTime? UserVehicleBoundTime { get; set; }
        public DateTime? LastDCMUseTime { get; set; }
    }

    public class CustomerInfo
    {
        public string UserId { get; set; }
        public string Language { get; set; }
        public string Timezone { get; set; }
        public string RegionCode { get; set; }
        public string OwnerId { get; set; }
        public string Nickname { get; set; }
        public string Country { get; set; }
        public string VehicleImage { get; set; }
        public string UserVehicleBoundDurationSec { get; set; }
        public CustomerVehicleInfo VehicleInfo { get; set; }
    }

    public class LoginResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("sessionId")]
        public Guid SessionGuid { get; set; }

        [JsonProperty("VehicleInfoList")]
        [JsonConverter(typeof(VehicleInfoListConverter))]
        public IEnumerable<VehicleInfo> VehicleInfos { get; set; }

        [JsonProperty("vehicle")]
        public Vehicle Vehicle { get; set; }

        [JsonProperty("EncAuthToken")]
        public string EncAuthToken { get; set; }

        public CustomerInfo CustomerInfo { get; set; }

        public string UserInfoRevisionNo { get; set; }
    }
}
