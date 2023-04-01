using ERP_Core;

namespace ERP_Schedule
{
    public class Time
    {
        private int _hour;
        private int _minute;

        public int Hour => _hour;
        public int Minute => _minute;

        public Time(int hour, int minute)
        {
            _hour = hour;
            _minute = minute;
        }
        public readonly static Time Seven= new Time(7,0);
        public readonly static Time Eight= new Time(8,0);
        public readonly static Time Nine= new Time(9,0);
        public readonly static Time Thirdteen= new Time(13,0);
        public readonly static Time Fourteen= new Time(14,0);
        public readonly static Time Fifteen= new Time(15,0);
        public readonly static Time Sixteen= new Time(16,0);
        public readonly static Time Seventeen= new Time(17,0);
        public readonly static Time TwentyOne= new Time(21,0);
        public readonly static Time TwentyTwo= new Time(22,0);
        public readonly static Time TwentyThree= new Time(23,0);
        public readonly static Time Zero= new Time(0,0);
    }
    public class ScheduleType
    {
        Time _startTime;
        Time _endTime;
        public Time StartTime => _startTime;
        public Time EndTime => _endTime;
        public ScheduleType(Time start, Time end)
        {
            _startTime = start;
            _endTime = end;
        }
    }

    public class ScheduleData
    {
        public Employee Member;
        public float Weight;
        public DateOnly Date;
        public ScheduleType ScheduleType;
        public ScheduleType PresetScheduleType;

    }
    public class ScheduleManager
    {
        public Dictionary<string, ScheduleType> AllScheduleTypes= new Dictionary<string, ScheduleType>();
        public void Initialize()
        {
            AllScheduleTypes.Add("A1", new ScheduleType(Time.Seven, Time.Fifteen));
            AllScheduleTypes.Add("A2", new ScheduleType(Time.Eight, Time.Sixteen));
            AllScheduleTypes.Add("A3", new ScheduleType(Time.Nine, Time.Seventeen));
            AllScheduleTypes.Add("B1", new ScheduleType(Time.Fourteen, Time.TwentyTwo));
            AllScheduleTypes.Add("B2", new ScheduleType(Time.Fifteen, Time.TwentyThree));
            AllScheduleTypes.Add("B3", new ScheduleType(Time.Sixteen, Time.Zero));
            AllScheduleTypes.Add("C", new ScheduleType(Time.TwentyThree, Time.Seven));
        }
        public void Assign() { }
    }
}