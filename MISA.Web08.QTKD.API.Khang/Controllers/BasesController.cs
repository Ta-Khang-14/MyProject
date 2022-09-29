using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.QTKD.BL.Khang;

namespace MISA.Web08.QTKD.API.Khang
{
    [Route("api/[controller]")]
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
            try
            {
                var data = _baseBL.Records();
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, err);
            }
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
            try
            {
                var data = _baseBL.Record(recordID);
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, err);
            }
        }
        #endregion


    }
}
