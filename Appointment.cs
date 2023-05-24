using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW2
{
    public class Appointment
    {
        public int ID_appointment { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int ProcedureID { get; set; }
        public string DDescription { get; set; }
        public DateTime AppointmentDate { get; set; }
    }

}
