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
            var data = _baseBL.Records();
            if (data != null)
            {
                return StatusCode(StatusCodes.Status200OK, data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));
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
            var data = _baseBL.Record(recordID);

            if (data != null)
            {
                return StatusCode(StatusCodes.Status200OK, data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, ErrorResult.Generate500Error(HttpContext.TraceIdentifier));

        }
        #endregion


    }
}
