using System.ComponentModel.DataAnnotations;

#region BodyMeasurementsClass
namespace HealthAndFitnessTracker.Classes
{
    public class BodyMeasurements
    {
        #region Properties
        [Key]
        public int BodymeasurementId { get; set; }
        public DateTime BodymeasurementDate { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double BodyfatPercentage { get; set; }
        public int PersonId { get; set; }
        #endregion

        #region Constructors
        public BodyMeasurements() { }
        public BodyMeasurements(DateTime bodymeasurementDate, double weight, double height, double bodyfatPercentage, int PersonId)
        {

            BodymeasurementDate = bodymeasurementDate;
            Weight = weight;
            Height = height;
            BodyfatPercentage = bodyfatPercentage;
            this.PersonId = PersonId;
        }
        #endregion
    }
}
#endregion