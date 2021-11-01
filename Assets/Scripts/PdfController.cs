using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Spire.Pdf;



public class PdfController : MonoBehaviour
{
    public InputField pdfAdress;

    public Image pageImage;


    public GameObject newimage;
    public Transform receiver;

    public void GetPdf()
    {
        string path = pdfAdress.text;

        Debug.Log("allons chercher la photo à " + path);


        PdfDocument doc = new PdfDocument();
        if (System.IO.File.Exists(path))
        {
            doc.LoadFromFile(path);
        }
        else
        {
            Debug.Log("le fichier demandé n'existe pas");
        }

        doc.SaveAsImage(0);

        PdfPageBase page = doc.Pages[0];

        Stream[] images = page.ExtractImages();
        


        List<Sprite> imageList = new List<Sprite>();

        for (int i = 0; i < images.Length; i++)
        {
            BinaryReader streamrd = new BinaryReader(images[i]);

            byte[] b = null;

            using (StreamReader reader = new StreamReader(images[i]))
            {
                using (var memstream = new MemoryStream())
                {
                    var buffer = new byte[512];
                    var bytesRead = default(int);
                    while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                        memstream.Write(buffer, 0, bytesRead);
                    b = memstream.ToArray();
                }
            }






            //byte[] b = null;


            //    int count = 0;
            //    do
            //    {
            //        byte[] buf = new byte[1024];

            //        b = streamrd.ReadBytes
            //        count = streamrd.Read(buf, 0, count);
            //        ms.Write(buf, 0, count);

            //    } while (stream.CanRead && count > 0);
            //    b = ms.ToArray();




            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(b);
            byte[] pngByte = tex.EncodeToPNG();
            File.WriteAllBytes("D:\\UnityProjects\\JDRCBup\\Assets\\Sprites" +"\\3A_Eleve_"  + i + ".png", pngByte);
            Sprite img = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            imageList.Add(img);
        }

        StartCoroutine(LoadImages(imageList));
    }
    IEnumerator LoadImages(List<Sprite> list)
    {
        foreach(Sprite s in list)
        {

            GameObject newImg = Instantiate(newimage, receiver);
            newImg.GetComponent<Image>().sprite = s;
            yield return new WaitForSeconds(1);
        }
    }
        
}
