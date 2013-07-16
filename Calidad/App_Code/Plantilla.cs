using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HPSF;
using NPOI.HSSF.Record.Crypto;
using NPOI.POIFS.FileSystem;

/// <summary>
/// Summary description for Plantilla
/// </summary>
public class Plantilla
{
    public Plantilla() { }
    public Plantilla(String plantilla, DataSet ds, HttpResponse Response)
    {
        string filename = DateTime.Now.ToString().Replace(" ","").Replace(":","").Replace("/","");
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename+".xls"));
        Response.Clear();
        InitializeWorkbook();
        switch (plantilla)
        {
            case "Inverndaderos": invernaderos(ds); break;
            default: 
                    break;
                
        }
        Response.BinaryWrite(WriteToStream().GetBuffer());
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }


    #region Estilos

    HSSFWorkbook hssfworkbook;
     ICellStyle EstTitulos;
     ICellStyle EstSubtitulos;
     ICellStyle EstCeldas;
     ICellStyle EstCeldasDate;
     ICellStyle EstCeldasP;
     ICellStyle EstCeldasPercentLocked;
     ICellStyle EstInvernadero;

    MemoryStream WriteToStream()
    {
        //Write the stream data of workbook to the root directory
        MemoryStream file = new MemoryStream();
        hssfworkbook.Write(file);
        
        return file;
    }
    public bool protectSheet(string url, string sheetName)
    {
        HSSFWorkbook hssfwb;
        using (FileStream file = new FileStream(@url, FileMode.Open, FileAccess.ReadWrite))
        {
            hssfwb = new HSSFWorkbook(file);
        }
        ISheet sheet = hssfwb.GetSheet(sheetName);
        sheet.ProtectSheet("<P@ssw0rd>");
        if(sheet.Protect)
            return true;
        else
            return false;
    }
    public bool protectSheet(string url, int sheetZeroIndex)
    {
        FileStream file = new FileStream(@url, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        hssfworkbook = new HSSFWorkbook(file);
        ISheet sheet = hssfworkbook.GetSheetAt(sheetZeroIndex);
        sheet.ProtectSheet("<P@ssw0rd>");
        sheet.SetActiveCell(0, 0);
        file = new FileStream(@url, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        hssfworkbook.Write(file);
        if (sheet.Protect)
        {
            file.Close();
            return true;
        }
        else
        {
            file.Close();
            return false;
        }
    }
    private void GenerarFormatos()
    {
        HSSFPalette Colores = hssfworkbook.GetCustomPalette();

        Colores.SetColorAtIndex(HSSFColor.GREEN.index, (byte)83, (byte)147, (byte)91); //Verde
        Colores.SetColorAtIndex(HSSFColor.DARK_YELLOW.index, (byte)231, (byte)204, (byte)45); //Amarillo Fuerte
        Colores.SetColorAtIndex(HSSFColor.LIGHT_YELLOW.index, (byte)248, (byte)241, (byte)196); //Amarillo Suave
        Colores.SetColorAtIndex(HSSFColor.WHITE.index, (byte)255, (byte)255, (byte)255); //Blanco
        Colores.SetColorAtIndex(HSSFColor.BLACK.index, (byte)0, (byte)0, (byte)0); //Negro

        Colores.SetColorAtIndex(HSSFColor.GREY_25_PERCENT.index, (byte)216, (byte)228, (byte)188); //Verde Suave

        IFont FBlanca12 = hssfworkbook.CreateFont();
        FBlanca12.Color = HSSFColor.WHITE.index;
        FBlanca12.IsItalic = false;
        FBlanca12.FontHeightInPoints = 12;
        IFont FBlanca10 = hssfworkbook.CreateFont();
        FBlanca10.Color = HSSFColor.WHITE.index;
        FBlanca10.IsItalic = false;
        FBlanca10.FontHeightInPoints = 10;
        IFont FNegra = hssfworkbook.CreateFont();
        FNegra.Color = HSSFColor.BLACK.index;
        FNegra.IsItalic = false;
        FNegra.FontHeightInPoints = 10;
        
        EstTitulos = hssfworkbook.CreateCellStyle();
        EstTitulos.Alignment = HorizontalAlignment.CENTER;
        EstTitulos.FillPattern = FillPatternType.SOLID_FOREGROUND;
        EstTitulos.FillForegroundColor = HSSFColor.GREEN.index;
        EstTitulos.VerticalAlignment = VerticalAlignment.CENTER;
        EstTitulos.SetFont(FBlanca10);
        EstTitulos.IsLocked = true;

        EstInvernadero = hssfworkbook.CreateCellStyle();
        EstInvernadero.Alignment = HorizontalAlignment.CENTER;
        EstInvernadero.FillPattern = FillPatternType.SOLID_FOREGROUND;
        EstInvernadero.FillForegroundColor = HSSFColor.GREY_25_PERCENT.index;
        EstInvernadero.VerticalAlignment = VerticalAlignment.CENTER;
        EstInvernadero.SetFont(FNegra);
        EstInvernadero.IsLocked = true;       

        EstSubtitulos = hssfworkbook.CreateCellStyle();
        EstSubtitulos.Alignment = HorizontalAlignment.CENTER;
        EstSubtitulos.FillPattern = FillPatternType.SOLID_FOREGROUND;
        EstSubtitulos.FillForegroundColor = HSSFColor.DARK_YELLOW.index;
        EstSubtitulos.SetFont(FBlanca10);
        EstSubtitulos.IsLocked = true;
      

        EstCeldas = hssfworkbook.CreateCellStyle();
        EstCeldas.Alignment = HorizontalAlignment.CENTER;
        EstCeldas.FillPattern = FillPatternType.SOLID_FOREGROUND;
        EstCeldas.FillForegroundColor = HSSFColor.LIGHT_YELLOW.index;
        EstCeldas.SetFont(FNegra);
        EstCeldas.IsLocked = false;
        
        var formatId = HSSFDataFormat.GetBuiltinFormat("#,###,##0;-#,###,##0");
        if (formatId == -1)
        {
            var newDataFormat = hssfworkbook.CreateDataFormat();
            EstCeldas.DataFormat = newDataFormat.GetFormat("#,###,##0;-#,###,##0");
        }

        else
            EstCeldas.DataFormat = formatId;

        EstCeldasDate = hssfworkbook.CreateCellStyle();
        EstCeldasDate.Alignment = HorizontalAlignment.CENTER;
        EstCeldasDate.FillPattern = FillPatternType.SOLID_FOREGROUND;
        EstCeldasDate.FillForegroundColor = HSSFColor.LIGHT_YELLOW.index;
        EstCeldasDate.SetFont(FNegra);
        EstCeldasDate.IsLocked = false;
        formatId = HSSFDataFormat.GetBuiltinFormat("m/d/yy");
        if (formatId == -1)
        {
            var newDataFormat = hssfworkbook.CreateDataFormat();
            EstCeldasDate.DataFormat = newDataFormat.GetFormat("m/d/yy");
        }

        else
            EstCeldasDate.DataFormat = formatId;


        EstCeldasP = hssfworkbook.CreateCellStyle();
        EstCeldasP.Alignment = HorizontalAlignment.CENTER;
        EstCeldasP.FillPattern = FillPatternType.SOLID_FOREGROUND;
        EstCeldasP.FillForegroundColor = HSSFColor.LIGHT_YELLOW.index;
        EstCeldasP.SetFont(FNegra);
        EstCeldasP.IsLocked = false;
        formatId = HSSFDataFormat.GetBuiltinFormat("0%");
        if (formatId == -1)
        {
            var newDataFormat = hssfworkbook.CreateDataFormat();
            EstCeldasP.DataFormat = newDataFormat.GetFormat("0%");
        }

        else
            EstCeldasP.DataFormat = formatId;
        EstCeldasPercentLocked = hssfworkbook.CreateCellStyle();
        EstCeldasPercentLocked.Alignment = HorizontalAlignment.CENTER;
        EstCeldasPercentLocked.FillPattern = FillPatternType.SOLID_FOREGROUND;
        EstCeldasPercentLocked.FillForegroundColor = HSSFColor.LIGHT_YELLOW.index;
        EstCeldasPercentLocked.SetFont(FNegra);
        EstCeldasPercentLocked.IsLocked = true;
        
        formatId = HSSFDataFormat.GetBuiltinFormat("0%");
        if (formatId == -1)
        {
            var newDataFormat = hssfworkbook.CreateDataFormat();
            EstCeldasPercentLocked.DataFormat = newDataFormat.GetFormat("0%");
        }

        else
            EstCeldasPercentLocked.DataFormat = formatId;

        
      

      
    }
    private void InitializeWorkbook()
    {
        hssfworkbook = new HSSFWorkbook();

        ////create a entry of DocumentSummaryInformation
        DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        dsi.Company = "Nature Sweet";
        hssfworkbook.DocumentSummaryInformation = dsi;

        ////create a entry of SummaryInformation
        SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        si.Subject = "Plantilla - Template";
        hssfworkbook.SummaryInformation = si;
        GenerarFormatos();
    }
    #endregion 


    private void PlantilllaPXI(DataSet ds)
    {
  
        ISheet Hoja1 = hssfworkbook.CreateSheet("Configuración de Invernadero");
        IRow Fila1 = Hoja1.CreateRow(0);
        IRow Fila3 = Hoja1.CreateRow(2);
        IRow Fila4 = Hoja1.CreateRow(3);


        Fila3.CreateCell(1).SetCellValue("Clave");
        Fila3.CreateCell(2).SetCellValue("Superficie");
        Fila3.CreateCell(3).SetCellValue("División");
        Fila3.CreateCell(4).SetCellValue("Sustrato Colocado");
        Fila3.CreateCell(5).SetCellValue("Vida del Sustrato");
        Fila3.CreateCell(6).SetCellValue("Variable");
        Fila3.CreateCell(7).SetCellValue("Interplantable");
        ICell thisCell = null;

        thisCell = Fila4.CreateCell(1, CellType.STRING); thisCell.SetCellValue("5A01"); thisCell.CellStyle = EstCeldas;
        thisCell = Fila4.CreateCell(2, CellType.NUMERIC); thisCell.SetCellValue(1); thisCell.CellStyle = EstCeldas;
        thisCell = Fila4.CreateCell(3, CellType.STRING); thisCell.SetCellValue("CL1"); thisCell.CellStyle = EstCeldas;

        ICell dateCell = Fila4.CreateCell(4, CellType.NUMERIC); dateCell.SetCellValue(DateTime.Now); dateCell.CellStyle = EstCeldasDate;

        thisCell = Fila4.CreateCell(5, CellType.NUMERIC); thisCell.SetCellValue(52); thisCell.CellStyle = EstCeldas;
        thisCell = Fila4.CreateCell(6, CellType.STRING); thisCell.SetCellValue("V05"); thisCell.CellStyle = EstCeldas;
        thisCell = Fila4.CreateCell(7, CellType.STRING); thisCell.SetCellValue("Si"); thisCell.CellStyle = EstCeldas;



        int items = ds.Tables[0].Rows.Count;
        for (int c = 0; c < (items); c++)
        {
            Fila3.CreateCell(c + 8).SetCellValue(ds.Tables[0].Rows[c]["Product"].ToString().Trim());
            if (c % 3 == 0)
            {
                ICell newCell = Fila4.CreateCell(c + 8); newCell.SetCellValue("Si"); newCell.CellStyle = EstCeldas;
            }
            else
            {
                ICell newCell = Fila4.CreateCell(c + 8); newCell.SetCellValue("No"); newCell.CellStyle = EstCeldas;
            }
        }

        foreach (ICell x in Fila3.Cells)
            x.CellStyle = EstSubtitulos;
        for (int i = 0; i < Fila3.Cells.Count; i++)
            Hoja1.AutoSizeColumn(i + 1);
     
        IName name = hssfworkbook.CreateName();
        name.NameName = "PXI";
        name.RefersToFormula = "'Configuración de Invernadero'"+"!$B$3:$" + getChar(items + 8) + "$130";

        Fila1.CreateCell(1).SetCellValue("Configuración de Invernadero");
        Hoja1.AddMergedRegion(new CellRangeAddress(0, 1, 1, 7));
        Fila1.CreateCell(8).SetCellValue("Productos");
        Hoja1.AddMergedRegion(new CellRangeAddress(0, 1, 8, items + 7));
        Fila1.Cells[0].CellStyle = EstTitulos;
        Fila1.Cells[1].CellStyle = EstTitulos;


        for (int i = 4; i < 130; i++)
        {
            IRow row = Hoja1.CreateRow(i);
            for (int j = 1; j < (items + 8); j++)
            {
                ICell cell = row.CreateCell(j);
                cell.CellStyle = EstCeldas;
            }
        }
        Hoja1.ProtectSheet("<P@ssw0rd>");
      

    }

    private void invernaderos(DataSet ds)
    {
        ISheet Hoja1 = hssfworkbook.CreateSheet("Hoja1");

        IRow r0 = Hoja1.CreateRow(1);
        r0.CreateCell(1).SetCellValue("Planta");
        r0.Cells[0].CellStyle = EstTitulos;

        r0.CreateCell(2).SetCellValue(ds.Tables[0].Rows[1]["Name"].ToString().Trim());
        r0.Cells[1].CellStyle = EstTitulos;

        IRow r1 = Hoja1.CreateRow(2);
        r1.CreateCell(1).SetCellValue(ds.Tables[0].Rows[1]["Farm"].ToString().Trim());
        r1.Cells[0].CellStyle = EstCeldasPercentLocked;
        
        IRow r3 = Hoja1.CreateRow(5);
        r3.CreateCell(1).SetCellValue("Invernadero");
        r3.Cells[0].CellStyle = EstTitulos;

        r3.CreateCell(2).SetCellValue("Superficie");
        r3.Cells[1].CellStyle = EstTitulos;

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            IRow r5 = Hoja1.CreateRow(i + 6);
            r5.CreateCell(1).SetCellValue(ds.Tables[0].Rows[i]["invernadero"].ToString().Trim());
            r5.CreateCell(2).SetCellValue(ds.Tables[0].Rows[i]["Hectares"].ToString().Trim());

            r5.Cells[0].CellStyle = EstInvernadero;
            r5.Cells[1].CellStyle = EstInvernadero;
            
        }
        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
        {
             r3.CreateCell(i+3).SetCellValue(ds.Tables[1].Rows[i]["nombre"].ToString().Trim());
             r3.Cells[i+2].CellStyle = EstSubtitulos;

            /*if(i > 0 && i%2 == 0)
             r3.Cells[i + 2].CellStyle = EstCeldasPercentLocked;    */
        }

        for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
        {
            

        }


        IName name = hssfworkbook.CreateName();
        name.NameName = "Volumen";
        name.RefersToFormula = "Hoja1!$B$6:$" + getChar(r3.Cells.Count + 1) + "$" + (Hoja1.LastRowNum +1);

        IName name2 = hssfworkbook.CreateName(); 
        name2.NameName = "Planta";
        name2.RefersToFormula = "Hoja1!$B$2:$B$3";
    }
    
    private string getChar(int n)
    {
        switch(n)
        {
            case 1: return "A";
            case 2: return "B";
            case 3: return "C";
            case 4: return "D";
            case 5: return "E";
            case 6: return "F";
            case 7: return "G";
            case 8: return "H";
            case 9: return "I";
            case 10: return "J";
            case 11: return "K";
            case 12: return "L";
            case 13: return "M";
            case 14: return "N";
            case 15: return "O";
            case 16: return "P";
            case 17: return "Q";
            case 18: return "R";
            case 19: return "S";
            case 20: return "T";
            case 21: return "U";
            case 22: return "V";
            case 23: return "W";
            case 24: return "X";
            case 25: return "Y";
            case 26: return "Z";
        }
        if(n>26)
            return getChar(n / 26) + getChar(n - 26);
        if(n>702)
            return getChar(n / 702) + getChar(n-(702*(n/702)));

        return "0";
    }
   
}