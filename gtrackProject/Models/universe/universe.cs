using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class universe
    {
        public int Vehicle_Id { get; set; }
        public Nullable<int> GPS_Product_Id { get; set; }
        public Nullable<System.DateTime> CurrentData_Datetime { get; set; }
        public Nullable<int> CorrectData_Id { get; set; }
        public Nullable<System.DateTime> CorrectData_Datetime { get; set; }
        public string CM_Command { get; set; }
        public string CM_Engine { get; set; }
        public string CM_Meter { get; set; }
        public string CM_Batt { get; set; }
        public Nullable<decimal> FuelLevel { get; set; }
        public string CM_Temp { get; set; }
        public Nullable<byte> TempLevel { get; set; }
        public string CM_GPS { get; set; }
        public string CM_SignalStatus { get; set; }
        public Nullable<short> Speed { get; set; }
        public Nullable<decimal> Direction { get; set; }
        public string IpGPS { get; set; }
        public Nullable<short> Port { get; set; }
        public Nullable<decimal> LaGoogle { get; set; }
        public Nullable<decimal> LongGoogle { get; set; }
        public byte Display_Status { get; set; }
        public Nullable<int> Driver_Id { get; set; }
        public Nullable<int> Order_Id { get; set; }
        public Nullable<int> FixOrder_Id { get; set; }
        public int Id { get; set; }
        public virtual driver driver { get; set; }
        public virtual fix_orders fix_orders { get; set; }
        public virtual order order { get; set; }
        public virtual product_gps product_gps { get; set; }
        public virtual un_cm_batt un_cm_batt { get; set; }
        public virtual un_cm_comm un_cm_comm { get; set; }
        public virtual un_cm_engine un_cm_engine { get; set; }
        public virtual un_cm_gps un_cm_gps { get; set; }
        public virtual un_cm_meter un_cm_meter { get; set; }
        public virtual un_cm_signal un_cm_signal { get; set; }
        public virtual un_cm_temp un_cm_temp { get; set; }
        public virtual un_display_status un_display_status { get; set; }
        public virtual vehicle vehicle { get; set; }
    }
}
