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

        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = BirthProbCreate(@"C:\Temp\születés.csv");
            DeathProbabilities = DeathProbCreate(@"C:\Temp\halál.csv");

            for (int year = 2005; year <= 2024; year++)
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    // Ide jön a szimulációs lépés
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
    }
}
