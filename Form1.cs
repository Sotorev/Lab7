namespace Lab7
{
    public partial class Form1 : Form
    {
        List<Owner> owners = new List<Owner>();
        List<Ownership> ownerships = new List<Ownership>();
        List<Data> dataList = new List<Data>();
        bool switchOrderByMantenance = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void ReadOwnerFile()
        {
            FileStream fs = new FileStream("Owners.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while(sr.Peek() != -1)
            {
                Owner owner = new Owner();
                owner.DPI = sr.ReadLine();
                owner.Name = sr.ReadLine();
                owner.LastName = sr.ReadLine();
                owners.Add(owner);
            }
            fs.Close();
            sr.Close();

        }
        private void ReadOwnerShipFile()
        {
            FileStream fs = new FileStream("Ownership.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() != -1)
            {
                Ownership ownership = new Ownership();
                ownership.Id = Convert.ToInt32(sr.ReadLine());
                ownership.DPI = sr.ReadLine();
                ownership.MaintenanceFee = Convert.ToDouble(sr.ReadLine());
                ownerships.Add(ownership);
            }
            fs.Close();
            sr.Close();

        }
        private void AssignDataList()
        {
            foreach(var owner in owners)
            { 
            
                var ownership = ownerships.Find(x => x.DPI.Equals(owner.DPI));
                Data data = new Data()
                {
                    Id = ownership.Id,
                    LastName = owner.LastName,
                    Name = owner.Name,
                    MaintenanceFee = ownership.MaintenanceFee
                };
                dataList.Add(data);
            }
        }
        private void LoadDataGrid<T>(List<T> list)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = list;
            dataGridView1.Refresh();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadOwnerFile();
            ReadOwnerShipFile();
            AssignDataList();
            LoadDataGrid<Data>(dataList);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switchOrderByMantenance = !switchOrderByMantenance;
            if (switchOrderByMantenance)
            {
                LoadDataGrid<Data>(dataList.OrderByDescending(data => data.MaintenanceFee).ToList());
            }
            else
            {
                LoadDataGrid<Data>(dataList.OrderBy(data => data.MaintenanceFee).ToList());

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Data> dataList = this.dataList.OrderByDescending(d => d.MaintenanceFee).ToList();
            MessageBox.Show("" + dataList[0].MaintenanceFee + ',' 
                + dataList[1].MaintenanceFee +','+dataList[2].MaintenanceFee);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Data> dataList = this.dataList.OrderBy(d => d.MaintenanceFee).ToList();
            MessageBox.Show("" + dataList[0].MaintenanceFee + ','
                + dataList[1].MaintenanceFee + ',' + dataList[2].MaintenanceFee);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Data> dataList = this.dataList.OrderByDescending(d => d.MaintenanceFee).ToList();
            MessageBox.Show(dataList[0].Name + " " + dataList[0].LastName +
                "," + dataList[0].MaintenanceFee);
        }
    }
}