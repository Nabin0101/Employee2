using Data_Access_Layer.Model;
using Infrastructure.Common.ViewModel.Position;
using Infrastructure.Common.ViewModel.ResponseModel;

namespace Business_Layer.Position
{
    public interface IPosition
    {
        Task<APIResponseModel> AddPosition(PositionDTO positionDTO);
        Task<APIResponseModel> GetAllPositions();
    }
}
