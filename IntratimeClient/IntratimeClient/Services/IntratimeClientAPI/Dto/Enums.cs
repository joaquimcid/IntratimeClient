namespace IntratimeClient.Services.IntratimeClientAPI.Dto
{
    public enum RequestStatus
    {
        NotExists = 0,
        Requested = 1,
        Succeed = 2,
        Failed = 3,
    }

    public enum UserAction
    {
        Entrada = 0,
        Salida = 1,
        Pause = 2,
        Volver = 3,
        CheckPoint = 5,
    }
}