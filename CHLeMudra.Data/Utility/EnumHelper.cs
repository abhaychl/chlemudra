namespace CHLeMudra.Data
{
    public enum Roles
    {
        Admin = 1,        
        User = 2,       
    }
    public enum JobStatus
    {
        Ready = 0,        
        Sent = 1,
        Failed=2
    }

    public enum ProcessQueueJob
    {
        ImportExcelJob = 1,
        ExportExcelJob = 2,
        SenderJob = 3
    }

    public enum ProcessQueueJobStatus
    {
        Stop = 0,
        Running = 1,
        
       
    }
}
