using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResponseAnalyzer
{
    public class Analysis
    {
        public DeviceType DeviceType { get; set; }
        public NetworkType NetworkType { get; set; }
        public PlatformType PlatformType { get; set; }
        public SpeedCondition SpeedCondition { get; set; }
        public NetworkCondition NetworkCondition { get; set; }
        public ITFacility ITFacility { get; set; }
        public Area Area { get; set; }
        public Analysis()
        {
            DeviceType = new DeviceType();
            NetworkCondition = new NetworkCondition();
            NetworkType = new NetworkType();
            PlatformType = new PlatformType();
            SpeedCondition = new SpeedCondition();
            ITFacility = new ITFacility();
            Area = new Area();
        }
    }
    public class DeviceType
    {
        public int OnlySmartphone { get; set; }
        public int OnlyLaptop { get; set; }
        public int OnlyDesktop { get; set; }
        public int OnlyTablet { get; set; }
        public int None { get; set; }
        public int HasDevice { get; set; }
    }
    public class NetworkType
    {
        public int Only4G { get; set; }
        public int Only3G { get; set; }
        public int Only2G { get; set; }
        public int OnlyWifi { get; set; }
        public int OnlyBroadband { get; set; }
        public int None { get; set; }
        public int HasNetwork { get; set; }
    }
    public class PlatformType
    {
        public int Zoom { get; set; }
        public int Classroom { get; set; }
        public int Team { get; set; }
        public int Drive { get; set; }
    }
    public class SpeedCondition
    {
        public int Excellent { get; set; }
        public int Good { get; set; }
        public int Average { get; set; }
        public int Bad { get; set; }
        public int Worse { get; set; }
    }
    public class NetworkCondition
    {
        public int Yes { get; set; }
        public int No { get; set; }
        public int Has4Gor3GwithNo { get; set; }
        public int HasWifiWithNo { get; set; }
    }
    public class ITFacility
    {
        public int Yes { get; set; }
        public int No { get; set; }
        public int NoFromUnion { get; set; }
        public int NoFromUpazila { get; set; }
        public int NoFromDistrict { get; set; }
        public int NoFromDivision { get; set; }
        public int NoWithGoodConnection { get; set; }
        public int NoWithExcellentConnection { get; set; }
        public int NoWithAverageConnection { get; set; }

    }
    public class Area
    {
        public int Division { get; set; }
        public int District { get; set; }
        public int Upazila { get; set; }
        public int Union { get; set; }
    }
}
