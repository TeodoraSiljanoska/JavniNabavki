using JavniNabavki.Models;
using JavniNabavki.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Text;

namespace JavniNabavki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IExamRepository _examRepository;

        public ExcelController(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        [Route("ReadFile")]
        [HttpPost]
        public async Task<string> ReadFile(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            try
            {
                #region Variable Declaration
                string message = "";
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                Stream FileStream = null;
                #endregion

                #region Save Student Detail From Excel

                if (file.Length > 0)
                {

                    FileStream = file.OpenReadStream();

                    if (file != null && FileStream != null)
                    {
                        if (file.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (file.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            message = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtExamRecords = dsexcelRecords.Tables[0];
                            for (int i = 0; i < dtExamRecords.Rows.Count; i++)
                            {
                                Exam objExam = new Exam();
                                objExam.Id = Convert.ToInt32(dtExamRecords.Rows[i][0]);
                                objExam.Tip = Convert.ToString(dtExamRecords.Rows[i][1]);
                                objExam.Mesec = Convert.ToString(dtExamRecords.Rows[i][2]);

                                objExam.Pocetok = (Convert.ToDateTime(dtExamRecords.Rows[i][3])).Date;
                                objExam.Kraj = (Convert.ToDateTime(dtExamRecords.Rows[i][4])).Date;
                                objExam.Datum = (Convert.ToDateTime(dtExamRecords.Rows[i][5])).Date;
                                objExam.Ispit = (Convert.ToDateTime(dtExamRecords.Rows[i][6])).Date;
                                objExam.PopravenIspit = (Convert.ToDateTime(dtExamRecords.Rows[i][7])).Date;
                                var newExam = await _examRepository.Create(objExam);

                                if (newExam != null)
                                    message = "The Excel file has been successfully uploaded.";
                                else
                                    message = "Something Went Wrong!, The Excel file uploaded has failed.";
                            }
                        }
                        else
                            message = "Selected file is empty.";
                    }
                    else
                        message = "Invalid File.";
                }
                else
                    message = "Bad request.";

                return message;
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}