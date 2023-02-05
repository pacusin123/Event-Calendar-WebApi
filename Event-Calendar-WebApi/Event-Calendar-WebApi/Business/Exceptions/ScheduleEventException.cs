namespace Event_Calendar_WebApi.Business.Exceptions
{
    public class ScheduleEventException: Exception
    {
        public ScheduleEventException( string message)
            : base(message)
        {

        }
    }
}
