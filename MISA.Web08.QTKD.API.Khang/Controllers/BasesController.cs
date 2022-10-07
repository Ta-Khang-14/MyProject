using Microsoft.AspNetCore.Mvc;
using MISA.Web08.QTKD.Common.Khang;
using MISA.WEB08.QTKD.BL.Khang;

namespace MISA.Web08.QTKD.API.Khang
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Contructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Method

        /// <summary>
        /// Lấy tất cả bản ghi của 1 bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi của 1 mảng</returns>
        /// Created by: TVK(29/09/22)
        [HttpGet]
        [Route("")]
        public IActionResult Records()
        {
            ResponseHandle rs = _baseBL.Records(HttpContext.TraceIdentifier);

            // Kiểm tra thực hiện request thành công hay chưa
            if (rs.IsSuccess)
            {
                PagingData<T> data = (PagingData<T>)rs.Data;
                return StatusCode(StatusCodes.Status200OK, data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, rs.ErrorResult);
        }

        /// <summary>
        /// Lấy chi tiết 1 bản ghi theo ID
        /// </summary>
        /// <returns>Lấy chi tiết 1 bản ghi theo ID</returns>
        /// Created by: TVK(29/09/22)
        [HttpGet]
        [Route("{recordID}")]
        public IActionResult Record([FromRoute] Guid recordID)
        {
            ResponseHandle rs = _baseBL.Record(recordID, HttpContext.TraceIdentifier);

            // Kiểm tra thực hiện request thành công hay chưa
            if (rs.IsSuccess)
            {
                T data = (T)rs.Data;
                return StatusCode(StatusCodes.Status200OK, (T)data);
            }

            // Request thất bại
            if (rs.Status == 400)
            {
                return StatusCode(StatusCodes.Status400BadRequest, rs.ErrorResult);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, rs.ErrorResult);

        }
        #endregion


    }
}
