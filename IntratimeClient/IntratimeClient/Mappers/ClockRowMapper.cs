using IntratimeClient.Services.IntratimeClientAPI.Dto;
using IntratimeClient.Services.IntratimeClientAPI.Response.Query;
using IntratimeClient.ViewModel;

namespace IntratimeX.Mappers
{
    public class ClockRowMapper : BaseMapper<ClockRecordCell, ClockQueryResponse>
    {
        public override ClockQueryResponse Map(ClockRecordCell dto)
        {
            throw new System.NotImplementedException();
        }

        public override ClockRecordCell Map(ClockQueryResponse entity)
        {
            var result = new ClockRecordCell()
            {
                Status = RequestStatus.Succeed,
                UserAction = (UserAction)entity.InoutType,
                Time = entity.InoutDate.DateTime,
            };

            return result;
        }
    }
}