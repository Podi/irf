using fd64yt_irf_week9.Entities;
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

namespace fd64yt_irf_week9
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        Random rng = new Random(1234);

        List<int> malecount = new List<int>();
        List<int> femalecount = new List<int>();
        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = BirthProbCreate(@"C:\Temp\születés.csv");
            DeathProbabilities = DeathProbCreate(@"C:\Temp\halál.csv");
        }

        private void Simulation()
        {
            for (int year = 2005; year <= 2024; year++)
            {
                for (int j = 0; j < Population.Count; j++)
                {
                    Person p = new Person();
                    p = Population[j];
                    SimStep(j, p);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }
        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        ChildNum = byte.Parse(line[2])
                    });
                }
            }

            return population;
        }

        private List<BirthProbability> BirthProbCreate(string csvpath)
        {
            List<BirthProbability> birth = new List<BirthProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    BirthProbability bp = new BirthProbability();
                    bp.Age = int.Parse(line[0]);
                    bp.ChildNum = byte.Parse(line[1]);
                    bp.P = double.Parse(line[2]);
                    BirthProbabilities.Add(bp);
                }
            }
            return birth;
        }

        private List<DeathProbability> DeathProbCreate(string csvpath)
        {
            List<DeathProbability> death = new List<DeathProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    DeathProbability dp = new DeathProbability();
                    dp.Gender = (Gender)Enum.Parse(typeof(Gender), line[0]);
                    dp.Age = int.Parse(line[1]);
                    dp.P = double.Parse(line[2]);
                    DeathProbabilities.Add(dp);
                }
            }
            return death;
        }

        private void SimStep(int year, Person p)
        {
            if (p.IsAlive == false) return;

            int age = year - p.BirthYear;

            var deathProb = (from d in DeathProbabilities
                             where d.Gender == p.Gender && d.Age == age
                             select d.P).FirstOrDefault();
            if (rng.NextDouble() <= deathProb)
            {
                p.IsAlive = false;
            }

            if (p.IsAlive && p.Gender.Equals(Gender.Female))
            {
                var birthProb = (from b in BirthProbabilities
                                 where b.Age == age
                                 select b.P).FirstOrDefault();
                if (rng.NextDouble() <= birthProb)
                {
                    Person ps = new Person();
                    ps.BirthYear = year;
                    ps.ChildNum = 0;
                    p.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(p);
                }
            }
            int malec = (from x in Population
                         where x.Gender == Gender.Male
                         select x).Count();
            malecount.Add(malec);
            int femalec = (from x in Population
                           where x.Gender == Gender.Female
                           select x).Count();
            femalecount.Add(femalec);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Simulation();
            DisplayResults();
        }

        private void DisplayResults()
        {
            string result = "";

            for (int i = 2005; i <= malecount.Count(); i++)
            {
                string r = "";
                int males = malecount[i];
                int females = femalecount[i];
                r = $"Szimulációs év: {i}\n\tFiúk: {males}\n\tLányok: {females}\n";
                result = result + r;
            }
            richTextBox1.Text = result;

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }
    }
}
