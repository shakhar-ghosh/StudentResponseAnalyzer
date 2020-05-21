using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResponseAnalyzer
{
    public class Student
    {
        public string Name { get; set; }
        public string Roll { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Series { get; set; }
        public string Section { get; set; }
        public Questionarie Answers { get; set; }
    }

    public class Questionarie
    {
        public ResidenceArea ResidenceArea { get; set; }
        public List<Device> Devices { get; set; }
        public bool IsNetworkAvailable { get; set; }
        public List<InternetService> InternetServices { get; set; }
        public InternetServiceCondition InternetServiceCondition { get; set; }
        public List<Platform> Platforms { get; set; }
        public bool IsOnlineClassPossible { get; set; }
    }
    public enum Platform
    {
        Zoom,
        Classroom,
        Team,
        Drive
    }
    public enum InternetServiceCondition
    {
        Excellent,
        Good,
        Average,
        Bad,
        Worse
    }
    public enum ResidenceArea
    {
        Division,
        District,
        Upazila,
        Union
    }
    public enum Device
    {
        Smartphone,
        Laptop,
        Desktop,
        Tablet,
        None
    }
    public enum InternetService
    {
        G4,
        G3,
        G2,
        Wifi,
        Broadband,
        None
    }
}
