using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace UserDefinedUtility
{
    public static class DatabaseUtility
    {
        /// <summary>
        ///         loads the Images table with the binary data and file name of the image specified by imagePath.
        ///         returns the ID of the image after it has sucesfully been entered in
        /// </summary>
        /// <param name="imagePath"></param>
        public static int ImageFilePut(string imagePath)
        {
            byte[] file;
            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {          
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }
            using (var conn = new SqlConnection(_connectionString))
            {
                int ret;
                var sqlCommandString = @"
                INSERT INTO Images 
                        (Name,ImageBinary) 
                VALUES  
                        (@name,@file) 
                SELECT Image_ID from Images WHERE Image_ID = SCOPE_IDENTITY()";
                using (var command = new SqlCommand(sqlCommandString, conn))
                {
                    command.Parameters.Add(new SqlParameter("@file", file));
                    command.Parameters.Add(new SqlParameter("@name", Path.GetFileName(imagePath)));
                    conn.Open();
                    using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        ret = reader.Read() ? reader.GetInt32(0) : -1;
                    }
                    conn.Close();
                }
                return ret;
            }
        }

        /// <summary>
        ///     Downloads the file to the local temp directory and returns the path to said file.
        ///     This will only work smoothly for images not exceeding 8Mb in size. There may be considerable latency for 
        ///     files exceeding this size.
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        public static string ImageFileGet(int imageID)
        {
            string fileName;
            FileStream stream;
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand("SELECT Name,ImageBinary FROM Images WHERE Image_ID = @imageId", connection)){
                command.Parameters.AddWithValue("@imageId", imageID);
                connection.Open();
                using (var reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    reader.Read();

                    var name = reader.GetString(0);
                    fileName = Path.Combine(Path.GetTempPath(), name);

                    //reader.GetBytes() returns the number of bytes read.
                    var buf = new Byte[reader.GetBytes(1, 0, null, 0, int.MaxValue)];
                    reader.GetBytes(1, 0, buf, 0, buf.Length);
                    stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                    stream.Write(buf, 0, buf.Length);
                }
            }
            return fileName;
        }

        private static string _connectionString
        {
            get
            {
		    return @"REDACTED";
            }
        }
    }
}
