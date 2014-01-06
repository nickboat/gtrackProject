namespace gtrackProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Asp_Id = c.String(nullable: false, maxLength: 128),
                        Hd_Id = c.Short(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(nullable: false, maxLength: 10),
                        Email = c.String(maxLength: 100),
                        CompanyName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.hds", t => t.Hd_Id, cascadeDelete: true)
                .Index(t => t.Hd_Id);
            
            CreateTable(
                "dbo.driver_category",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Value = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IDCard = c.Int(nullable: false),
                        ExpireCard = c.Short(),
                        TitleName = c.String(maxLength: 6),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastNmae = c.String(nullable: false, maxLength: 30),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        DriverIDCard = c.Int(nullable: false),
                        ZIPCode = c.Short(nullable: false),
                        Category_Id = c.Byte(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.driver_category", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.universe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vehicle_Id = c.Int(nullable: false),
                        GPS_Product_Id = c.Int(),
                        CurrentData_Datetime = c.DateTime(),
                        CorrectData_Id = c.Int(),
                        CorrectData_Datetime = c.DateTime(),
                        CM_Command = c.String(maxLength: 1, fixedLength: true),
                        CM_Engine = c.String(maxLength: 1, fixedLength: true),
                        CM_Meter = c.String(maxLength: 1, fixedLength: true),
                        CM_Batt = c.String(maxLength: 1, fixedLength: true),
                        FuelLevel = c.Decimal(precision: 18, scale: 2),
                        CM_Temp = c.String(maxLength: 1, fixedLength: true),
                        TempLevel = c.Byte(),
                        CM_GPS = c.String(maxLength: 1, fixedLength: true),
                        CM_SignalStatus = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Speed = c.Short(),
                        Direction = c.Decimal(precision: 18, scale: 2),
                        IpGPS = c.String(maxLength: 21),
                        Port = c.Short(),
                        LaGoogle = c.Decimal(precision: 18, scale: 2),
                        LongGoogle = c.Decimal(precision: 18, scale: 2),
                        Display_Status = c.Byte(nullable: false),
                        Driver_Id = c.Int(),
                        Order_Id = c.Int(),
                        FixOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.drivers", t => t.Driver_Id)
                .ForeignKey("dbo.fix_orders", t => t.FixOrder_Id)
                .ForeignKey("dbo.orders", t => t.Order_Id)
                .ForeignKey("dbo.product_gps", t => t.GPS_Product_Id)
                .ForeignKey("dbo.un_cm_batt", t => t.CM_Batt)
                .ForeignKey("dbo.un_cm_comm", t => t.CM_Command)
                .ForeignKey("dbo.un_cm_engine", t => t.CM_Engine)
                .ForeignKey("dbo.un_cm_gps", t => t.CM_GPS)
                .ForeignKey("dbo.un_cm_meter", t => t.CM_Meter)
                .ForeignKey("dbo.un_cm_signal", t => t.CM_SignalStatus, cascadeDelete: true)
                .ForeignKey("dbo.un_cm_temp", t => t.CM_Temp)
                .ForeignKey("dbo.un_display_status", t => t.Display_Status, cascadeDelete: true)
                .ForeignKey("dbo.vehicles", t => t.Vehicle_Id, cascadeDelete: true)
                .Index(t => t.Driver_Id)
                .Index(t => t.FixOrder_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.GPS_Product_Id)
                .Index(t => t.CM_Batt)
                .Index(t => t.CM_Command)
                .Index(t => t.CM_Engine)
                .Index(t => t.CM_GPS)
                .Index(t => t.CM_Meter)
                .Index(t => t.CM_SignalStatus)
                .Index(t => t.CM_Temp)
                .Index(t => t.Display_Status)
                .Index(t => t.Vehicle_Id);
            
            CreateTable(
                "dbo.fix_orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateBy = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        CurrentUser = c.Int(),
                        HeadInstall = c.Int(nullable: false),
                        Comment = c.String(maxLength: 100),
                        Status = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.employee", t => t.CreateBy)
                .ForeignKey("dbo.employee", t => t.HeadInstall, cascadeDelete: true)
                .ForeignKey("dbo.order_status", t => t.Status)
                .Index(t => t.CreateBy)
                .Index(t => t.HeadInstall)
                .Index(t => t.Status);
            
            CreateTable(
                "dbo.employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 10),
                        AspId = c.String(nullable: false, maxLength: 128),
                        Gender = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.product_gps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SIM_Num = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        SIM_Brand_Id = c.Byte(),
                        SIM_Payment_Type_Id = c.Byte(),
                        Serial = c.String(nullable: false, maxLength: 10),
                        Version = c.Byte(nullable: false),
                        CreateBy = c.Int(),
                        CreateDate = c.DateTime(nullable: false),
                        StockBy = c.Int(),
                        StockDate = c.DateTime(),
                        QCBy = c.Int(),
                        QCDate = c.DateTime(),
                        InstallBy = c.Int(),
                        InstallDate = c.DateTime(),
                        ErrProductComment = c.String(maxLength: 150),
                        BadBy = c.Int(),
                        BadDate = c.DateTime(),
                        BadComment = c.String(maxLength: 150),
                        UnuseableBy = c.Int(),
                        UnuseableDate = c.DateTime(),
                        UnuseableComment = c.String(maxLength: 150),
                        ExpireDate = c.DateTime(),
                        LastExtendDate = c.DateTime(),
                        Status_Id = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.employee", t => t.StockBy)
                .ForeignKey("dbo.employee", t => t.BadBy)
                .ForeignKey("dbo.employee", t => t.QCBy)
                .ForeignKey("dbo.product_gps_type", t => t.Status_Id)
                .ForeignKey("dbo.product_gps_version", t => t.Version, cascadeDelete: true)
                .ForeignKey("dbo.employee", t => t.InstallBy)
                .ForeignKey("dbo.sim_brand", t => t.SIM_Brand_Id)
                .ForeignKey("dbo.sim_payment_type", t => t.SIM_Payment_Type_Id)
                .ForeignKey("dbo.employee", t => t.CreateBy)
                .ForeignKey("dbo.employee", t => t.UnuseableBy)
                .Index(t => t.StockBy)
                .Index(t => t.BadBy)
                .Index(t => t.QCBy)
                .Index(t => t.Status_Id)
                .Index(t => t.Version)
                .Index(t => t.InstallBy)
                .Index(t => t.SIM_Brand_Id)
                .Index(t => t.SIM_Payment_Type_Id)
                .Index(t => t.CreateBy)
                .Index(t => t.UnuseableBy);
            
            CreateTable(
                "dbo.product_camera",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serial = c.String(nullable: false, maxLength: 20),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.product_gps", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.product_gps_type",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        StatusName_TH = c.String(nullable: false, maxLength: 15),
                        StatusName_EN = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.product_gps_version",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CurrentUser = c.Int(),
                        HeadInstall = c.Int(nullable: false),
                        Hd_id = c.Short(nullable: false),
                        Version = c.Byte(),
                        Quantity = c.Int(nullable: false),
                        PricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FeePerYear = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(maxLength: 100),
                        Status = c.Byte(),
                        Deadline = c.DateTime(nullable: false),
                        ExtendType_Id = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.employee", t => t.CreateBy, cascadeDelete: true)
                .ForeignKey("dbo.employee", t => t.HeadInstall, cascadeDelete: true)
                .ForeignKey("dbo.hds", t => t.Hd_id, cascadeDelete: true)
                .ForeignKey("dbo.order_extend_type", t => t.ExtendType_Id)
                .ForeignKey("dbo.order_status", t => t.Status)
                .ForeignKey("dbo.product_gps_version", t => t.Version)
                .Index(t => t.CreateBy)
                .Index(t => t.HeadInstall)
                .Index(t => t.Hd_id)
                .Index(t => t.ExtendType_Id)
                .Index(t => t.Status)
                .Index(t => t.Version);
            
            CreateTable(
                "dbo.hds",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 3),
                        Code = c.String(nullable: false, maxLength: 6, fixedLength: true),
                        Name = c.String(maxLength: 50),
                        TableName = c.String(maxLength: 20),
                        HD_Id_upline = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.hds", t => t.HD_Id_upline)
                .Index(t => t.HD_Id_upline);
            
            CreateTable(
                "dbo.vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCar = c.String(nullable: false, maxLength: 6, fixedLength: true),
                        LicensePlate = c.String(maxLength: 8),
                        LicensePlate_Type = c.Byte(),
                        LicensePlate_At = c.Byte(),
                        ModelCar_Id = c.Short(),
                        ColorCar_Id = c.Byte(),
                        OganizeCar_Id = c.Byte(),
                        BodyNo = c.String(maxLength: 30),
                        HD_Id = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.hds", t => t.HD_Id, cascadeDelete: true)
                .ForeignKey("dbo.lp_type", t => t.LicensePlate_Type)
                .ForeignKey("dbo.province", t => t.LicensePlate_At)
                .ForeignKey("dbo.vehicle_color", t => t.ColorCar_Id)
                .ForeignKey("dbo.vehicle_model", t => t.ModelCar_Id)
                .ForeignKey("dbo.vehicle_oganize", t => t.OganizeCar_Id)
                .Index(t => t.HD_Id)
                .Index(t => t.LicensePlate_Type)
                .Index(t => t.LicensePlate_At)
                .Index(t => t.ColorCar_Id)
                .Index(t => t.ModelCar_Id)
                .Index(t => t.OganizeCar_Id);
            
            CreateTable(
                "dbo.lp_type",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(maxLength: 20),
                        Meaning = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.province",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        ShortName = c.String(maxLength: 2, fixedLength: true),
                        ShortNameEN = c.String(maxLength: 3, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.vehicle_color",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.vehicle_model",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Brand_Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Year = c.Short(),
                        Type_Id = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.vehicle_brand", t => t.Brand_Id, cascadeDelete: true)
                .ForeignKey("dbo.vehicle_type", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Brand_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.vehicle_brand",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.vehicle_type",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Head_Id = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.vehicle_head_type", t => t.Head_Id, cascadeDelete: true)
                .Index(t => t.Head_Id);
            
            CreateTable(
                "dbo.vehicle_head_type",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.vehicle_oganize",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.order_extend_type",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        TypeName = c.String(nullable: false, maxLength: 10),
                        Value = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.order_status",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Status_TH = c.String(nullable: false, maxLength: 15),
                        Status_EN = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sim_brand",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        BrandName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sim_payment_type",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        PaymentName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.un_cm_batt",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Msg_TH = c.String(maxLength: 255),
                        Msg_EN = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.un_cm_comm",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Msg_TH = c.String(maxLength: 255),
                        Msg_EN = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.un_cm_engine",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Msg_TH = c.String(maxLength: 255),
                        Msg_EN = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.un_cm_gps",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Msg_TH = c.String(maxLength: 255),
                        Msg_EN = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.un_cm_meter",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Msg_TH = c.String(maxLength: 255),
                        Msg_EN = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.un_cm_signal",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Msg_TH = c.String(maxLength: 255),
                        Msg_EN = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.un_cm_temp",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Msg_TH = c.String(maxLength: 255),
                        Msg_EN = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.un_display_status",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.universe", "Vehicle_Id", "dbo.vehicles");
            DropForeignKey("dbo.universe", "Display_Status", "dbo.un_display_status");
            DropForeignKey("dbo.universe", "CM_Temp", "dbo.un_cm_temp");
            DropForeignKey("dbo.universe", "CM_SignalStatus", "dbo.un_cm_signal");
            DropForeignKey("dbo.universe", "CM_Meter", "dbo.un_cm_meter");
            DropForeignKey("dbo.universe", "CM_GPS", "dbo.un_cm_gps");
            DropForeignKey("dbo.universe", "CM_Engine", "dbo.un_cm_engine");
            DropForeignKey("dbo.universe", "CM_Command", "dbo.un_cm_comm");
            DropForeignKey("dbo.universe", "CM_Batt", "dbo.un_cm_batt");
            DropForeignKey("dbo.universe", "GPS_Product_Id", "dbo.product_gps");
            DropForeignKey("dbo.universe", "Order_Id", "dbo.orders");
            DropForeignKey("dbo.universe", "FixOrder_Id", "dbo.fix_orders");
            DropForeignKey("dbo.fix_orders", "Status", "dbo.order_status");
            DropForeignKey("dbo.fix_orders", "HeadInstall", "dbo.employee");
            DropForeignKey("dbo.fix_orders", "CreateBy", "dbo.employee");
            DropForeignKey("dbo.product_gps", "UnuseableBy", "dbo.employee");
            DropForeignKey("dbo.product_gps", "CreateBy", "dbo.employee");
            DropForeignKey("dbo.product_gps", "SIM_Payment_Type_Id", "dbo.sim_payment_type");
            DropForeignKey("dbo.product_gps", "SIM_Brand_Id", "dbo.sim_brand");
            DropForeignKey("dbo.product_gps", "InstallBy", "dbo.employee");
            DropForeignKey("dbo.product_gps", "Version", "dbo.product_gps_version");
            DropForeignKey("dbo.orders", "Version", "dbo.product_gps_version");
            DropForeignKey("dbo.orders", "Status", "dbo.order_status");
            DropForeignKey("dbo.orders", "ExtendType_Id", "dbo.order_extend_type");
            DropForeignKey("dbo.orders", "Hd_id", "dbo.hds");
            DropForeignKey("dbo.vehicles", "OganizeCar_Id", "dbo.vehicle_oganize");
            DropForeignKey("dbo.vehicles", "ModelCar_Id", "dbo.vehicle_model");
            DropForeignKey("dbo.vehicle_model", "Type_Id", "dbo.vehicle_type");
            DropForeignKey("dbo.vehicle_type", "Head_Id", "dbo.vehicle_head_type");
            DropForeignKey("dbo.vehicle_model", "Brand_Id", "dbo.vehicle_brand");
            DropForeignKey("dbo.vehicles", "ColorCar_Id", "dbo.vehicle_color");
            DropForeignKey("dbo.vehicles", "LicensePlate_At", "dbo.province");
            DropForeignKey("dbo.vehicles", "LicensePlate_Type", "dbo.lp_type");
            DropForeignKey("dbo.vehicles", "HD_Id", "dbo.hds");
            DropForeignKey("dbo.hds", "HD_Id_upline", "dbo.hds");
            DropForeignKey("dbo.orders", "HeadInstall", "dbo.employee");
            DropForeignKey("dbo.orders", "CreateBy", "dbo.employee");
            DropForeignKey("dbo.product_gps", "Status_Id", "dbo.product_gps_type");
            DropForeignKey("dbo.product_gps", "QCBy", "dbo.employee");
            DropForeignKey("dbo.product_gps", "BadBy", "dbo.employee");
            DropForeignKey("dbo.product_camera", "Product_Id", "dbo.product_gps");
            DropForeignKey("dbo.product_gps", "StockBy", "dbo.employee");
            DropForeignKey("dbo.universe", "Driver_Id", "dbo.drivers");
            DropForeignKey("dbo.drivers", "Category_Id", "dbo.driver_category");
            DropIndex("dbo.universe", new[] { "Vehicle_Id" });
            DropIndex("dbo.universe", new[] { "Display_Status" });
            DropIndex("dbo.universe", new[] { "CM_Temp" });
            DropIndex("dbo.universe", new[] { "CM_SignalStatus" });
            DropIndex("dbo.universe", new[] { "CM_Meter" });
            DropIndex("dbo.universe", new[] { "CM_GPS" });
            DropIndex("dbo.universe", new[] { "CM_Engine" });
            DropIndex("dbo.universe", new[] { "CM_Command" });
            DropIndex("dbo.universe", new[] { "CM_Batt" });
            DropIndex("dbo.universe", new[] { "GPS_Product_Id" });
            DropIndex("dbo.universe", new[] { "Order_Id" });
            DropIndex("dbo.universe", new[] { "FixOrder_Id" });
            DropIndex("dbo.fix_orders", new[] { "Status" });
            DropIndex("dbo.fix_orders", new[] { "HeadInstall" });
            DropIndex("dbo.fix_orders", new[] { "CreateBy" });
            DropIndex("dbo.product_gps", new[] { "UnuseableBy" });
            DropIndex("dbo.product_gps", new[] { "CreateBy" });
            DropIndex("dbo.product_gps", new[] { "SIM_Payment_Type_Id" });
            DropIndex("dbo.product_gps", new[] { "SIM_Brand_Id" });
            DropIndex("dbo.product_gps", new[] { "InstallBy" });
            DropIndex("dbo.product_gps", new[] { "Version" });
            DropIndex("dbo.orders", new[] { "Version" });
            DropIndex("dbo.orders", new[] { "Status" });
            DropIndex("dbo.orders", new[] { "ExtendType_Id" });
            DropIndex("dbo.orders", new[] { "Hd_id" });
            DropIndex("dbo.vehicles", new[] { "OganizeCar_Id" });
            DropIndex("dbo.vehicles", new[] { "ModelCar_Id" });
            DropIndex("dbo.vehicle_model", new[] { "Type_Id" });
            DropIndex("dbo.vehicle_type", new[] { "Head_Id" });
            DropIndex("dbo.vehicle_model", new[] { "Brand_Id" });
            DropIndex("dbo.vehicles", new[] { "ColorCar_Id" });
            DropIndex("dbo.vehicles", new[] { "LicensePlate_At" });
            DropIndex("dbo.vehicles", new[] { "LicensePlate_Type" });
            DropIndex("dbo.vehicles", new[] { "HD_Id" });
            DropIndex("dbo.hds", new[] { "HD_Id_upline" });
            DropIndex("dbo.orders", new[] { "HeadInstall" });
            DropIndex("dbo.orders", new[] { "CreateBy" });
            DropIndex("dbo.product_gps", new[] { "Status_Id" });
            DropIndex("dbo.product_gps", new[] { "QCBy" });
            DropIndex("dbo.product_gps", new[] { "BadBy" });
            DropIndex("dbo.product_camera", new[] { "Product_Id" });
            DropIndex("dbo.product_gps", new[] { "StockBy" });
            DropIndex("dbo.universe", new[] { "Driver_Id" });
            DropIndex("dbo.drivers", new[] { "Category_Id" });
            DropTable("dbo.un_display_status");
            DropTable("dbo.un_cm_temp");
            DropTable("dbo.un_cm_signal");
            DropTable("dbo.un_cm_meter");
            DropTable("dbo.un_cm_gps");
            DropTable("dbo.un_cm_engine");
            DropTable("dbo.un_cm_comm");
            DropTable("dbo.un_cm_batt");
            DropTable("dbo.sim_payment_type");
            DropTable("dbo.sim_brand");
            DropTable("dbo.order_status");
            DropTable("dbo.order_extend_type");
            DropTable("dbo.vehicle_oganize");
            DropTable("dbo.vehicle_head_type");
            DropTable("dbo.vehicle_type");
            DropTable("dbo.vehicle_brand");
            DropTable("dbo.vehicle_model");
            DropTable("dbo.vehicle_color");
            DropTable("dbo.province");
            DropTable("dbo.lp_type");
            DropTable("dbo.vehicles");
            DropTable("dbo.hds");
            DropTable("dbo.orders");
            DropTable("dbo.product_gps_version");
            DropTable("dbo.product_gps_type");
            DropTable("dbo.product_camera");
            DropTable("dbo.product_gps");
            DropTable("dbo.employee");
            DropTable("dbo.fix_orders");
            DropTable("dbo.universe");
            DropTable("dbo.drivers");
            DropTable("dbo.driver_category");
            DropTable("dbo.customer");
        }
    }
}
