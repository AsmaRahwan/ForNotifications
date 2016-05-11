using System;
using System.Net;
using System.Text;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace ForNotification
{
    public partial class _Default : System.Web.UI.Page
    {
        string ApplicationID;
        string SENDER_ID;
        string RegId;
        //Asmaa 28-01-2016
        protected void Page_Load(object sender, EventArgs e)
        {
            RegId = "APA91bEtRtMUbIA5J_s25Bq0jYc21XqrFXM6N1ojFpgqK187HmBY7quQKybtoTksechkVdiOkWrkn_g_YQavFA5tIP2s5TMZ_IOdqjpq1-MEGz1zrMahWHcFCxSCk9xrBya-BCUV_HLQ";
            ApplicationID = "AIzaSyDuCy3QJKhdbtX6vKb4TBxv1Fs_v2PB5vo";
            SENDER_ID = "891112643416";
            var value = TextBox1.Text; //message text box

            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send"); tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", ApplicationID));
            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
            //Data post to the Server
         //   string postData =
      //  "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
      //   + value + "&data.time=" + System.DateTime.Now.ToString() +
      //  "&to=" + RegId + "";
            /////////////////////////
            var payloadData = new
            {
                collapse_key = "score_update",
                priority = "high",
                time_to_live = 10,
                delay_while_idle = false,
                content_available = true,
                data = new
                {
                    message = value,
                    ID = 751,

                    time = DateTime.Now.ToString()
                },
                to = RegId
            };
            /////////////////////////
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string postData = jsSerializer.Serialize(payloadData);
            Console.WriteLine(postData);

            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;
            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse tResponse = tRequest.GetResponse();
            dataStream = tResponse.GetResponseStream();
            StreamReader tReader = new StreamReader(dataStream);
            String sResponseFromServer = tReader.ReadToEnd();  //Get response from GCM server  
            label_Result.Text = sResponseFromServer; //Assigning GCM response to Label text
            tReader.Close(); dataStream.Close();
            tResponse.Close();                                                                                                  
        }
    }
}