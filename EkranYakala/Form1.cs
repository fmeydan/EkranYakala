using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EkranYakala
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        string yol;  //Farklı fonksiyonlardan müdahale edebilmek için globalde yol u tutan string değişken.
        
        


        public void button1_Click(object sender, EventArgs e) //Yakala butonunun tıklanma fonksiyonu.
        {
            this.Hide(); //Yakalanan ekranda bulunmasın diye gizliyoruz.
            System.Threading.Thread.Sleep(500); //Pencerenin gizlenmesi için bir süre işlemi bekletiyoruz.
            Bitmap bm = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height); //Yakalanan görüntünün en boy oranını sistemimizin en boy oranıyla aynı olmasını sağlıyoruz. İki monitörlü sistemlerde Birincil monitörü yakalar.
            Graphics g = Graphics.FromImage(bm); //Bitmap ile alınan ekran verilerini görselleştiriyoruz.
            g.CopyFromScreen(0,0,0,0,bm.Size); //Görseli aldığımız ekran verilene göre oluşturuyoruz.
            pictureBox1.Image = bm; //oluşturduğumuz image box a küçük bir önizlemesini yerleştiriyoruz.
            string dosya_adi = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"); //dosya adının benzersiz olması için sistem tarih ve saatini kullanıyoruz.
            yol = Properties.Settings.Default.yol; //program ilk açılırken sakladığımız klasör bilgisini yol değişkenine atıyoruz.

            //MessageBox.Show(yol + dosya_adi); Kontrol için
            bm.Save(yol + "\\" + dosya_adi + ".jpg"); //oluiturduğumuz görüntüyü daha önceden belirlediğimiz yola kaydediyoruz.
            this.Show(); //penceremizi tekrar görünür hale getiriyoruz.
        }

        public void button2_Click(object sender, EventArgs e) //Yer Değiştir butonunun tıklanma fonksiyonu.
        {
            DialogResult result = MessageBox.Show("Kaydetme Yerini Seçin", "Ekran Yakala", MessageBoxButtons.OK); 
            if (result == DialogResult.OK)
            {
                SaveFileDialog sf = new SaveFileDialog(); //Kaydetme yerinin seçilebilmesi için açılan dialog penceresi.
                sf.InitialDirectory = "C:\\"; //Açılan dialog penceresinin ilk başlayacağı yeri C: olarak belirledik.
                sf.FileName = "Hedef Klasörü Seçin"; // Normalde burası seçilecek dosyanın ismini ön tanımlı olarak yazmamız gereken yer fakat burda dosya seçmekten ziyade klasör seçtiğimiz için buranın dokunulmasına gerek yok. Boş kalmaması ve kullanıcıyı biraz olsun yönlendirmek için böyle yazdım.

                if (sf.ShowDialog() == DialogResult.OK) // Klasör seçme penceresinin save tuşuna basılınca çalışacak kodlar.
                {
                    yol = Path.GetDirectoryName(sf.FileName); //Seçilen klasörün yolu yol değişkenine atanır.
                    Properties.Settings.Default.yol = yol; //yol'un hafızada kalması için uygulama Propertiesine kaydediyoruz.
                    Properties.Settings.Default.Save(); //Properties e kaydetme işlmeni onaylıyoruz.
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {           
            
            string son_kayitli_yol=Properties.Settings.Default.yol;// uygulama properties'indeki yol u "son_kayitli_yol" değişkenine atıyoruz.            
            if (son_kayitli_yol==null) //bu değişken boş veya dolumu kontrolü yapıyoruz. eğer boş ise henüz yol belirtilmemiş demektir. 
            {
                DialogResult result = MessageBox.Show("Kaydetme Yerini Seçin", "Ekran Yakala", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    SaveFileDialog sf = new SaveFileDialog(); //Kaydetme yerinin seçilebilmesi için açılan dialog penceresi.
                    sf.InitialDirectory = "C:\\"; //Açılan dialog penceresinin ilk başlayacağı yeri C: olarak belirledik.
                    sf.FileName = "Hedef Klasörü Seçin";// Normalde burası seçilecek dosyanın ismini ön tanımlı olarak yazmamız gereken yer fakat burda dosya seçmekten ziyade klasör seçtiğimiz için buranın dokunulmasına gerek yok. Boş kalmaması ve kullanıcıyı biraz olsun yönlendirmek için böyle yazdım.
                    if (sf.ShowDialog() == DialogResult.OK)// Klasör seçme penceresinin save tuşuna basılınca çalışacak kodlar.
                    {
                        yol = Path.GetDirectoryName(sf.FileName); //Seçilen klasörün yolu yol değişkenine atanır.
                        Properties.Settings.Default.yol = yol;//yol'un hafızada kalması için uygulama Propertiesine kaydediyoruz.
                        Properties.Settings.Default.Save(); //Properties e kaydetme işlmeni onaylıyoruz.
                    }
                }
            }
            else
            {
                this.Show(); //uygulama penceremiz görüntülenir.
            }
            
          
            
        }
    }
}
