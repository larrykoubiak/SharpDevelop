//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Drawing;
	
using System.Data;
using System.Data.OleDb;
	
using SharpReport;
using SharpReportCore;
	
using SharpQuery;
using SharpQuery.Collections;
using SharpQuery.SchemaClass;
	
/// <summary>
/// This class creates settings for a report
/// </summary>
/// <remarks>
/// 	created by - Forstmeier Peter
/// 	created on - 28.01.2005 10:31:01
/// </remarks>
using System.Windows.Forms;

namespace ReportGenerator {	
	public class ReportGenerator : object {
		
		//BaseSettingsPanel
		
		private string reportName;
		private string fileName;
		private string path;
		
		private GlobalEnums.enmReportType reportType;
		private GraphicsUnit graphicsUnit;
		
		//Database
		private GlobalEnums.enmPushPullModel dataModel;

		private string connectionString;
		private string catalogName;
		private string sqlString;
		
		private CommandType commandType;
		private SharpQueryProcedure sharpQueryProcedure;
		private ReportItemCollection reportItemCollection;
		private ColumnCollection columnCollection;
		
		private SharpQuerySchemaClassCollection queryParameters;
		public ReportGenerator() {	
		}
		
		public ReportModel FillReportModel (ReportModel model) {
			if (model == null) {
				throw new MissingModelException();
			}
			model.ReportSettings.ReportName = this.reportName;
			model.ReportSettings.FileName = this.path + this.fileName;
			
			model.ReportSettings.GraphicsUnit = this.graphicsUnit;
			model.ReportSettings.ReportType = this.reportType;

			model.ReportSettings.ConnectionString = this.connectionString;
			model.ReportSettings.CommandText = this.sqlString;
			model.ReportSettings.CommandType = this.commandType;
			model.ReportSettings.DataModel = this.dataModel;
			return model;
		}
		
		/// <summary>
		/// This methode build the ReportParameterCollection we use in ReportSettings
		/// from the queryParameters
		/// </summary>
		/// <returns></returns>
		public   AbstractParametersCollection BuildSqlParameterList(){			
			if (this.queryParameters != null && this.queryParameters.Count > 0) {
				AbstractParametersCollection paramCol = new  AbstractParametersCollection();
				SqlParameter reportPar;
				
				SharpQueryParameter sqPar = null;
				for (int i = 0;i < this.queryParameters.Count ;i++ ) {
					sqPar = (SharpQueryParameter) this.queryParameters[i];
					reportPar =  new SqlParameter (sqPar.Name,
					                               sqPar.DataType,
					                              null,
					                               String.Empty,
					                               sqPar.Type);
					
					reportPar.DefaultValue = sqPar.Value;
					paramCol.Add(reportPar);
					
				}
				return paramCol;
			}
			return null;
			
		}
		
		
		#region BaseSettingsPanel property's
		public GraphicsUnit GraphicsUnit {
			get {
				return graphicsUnit;
			}
			set {
				graphicsUnit = value;
			}
		}
		public string ReportName {
			get {
				return reportName;
			}
			set {
				reportName = value;
			}
		}
		public SharpReportCore.GlobalEnums.enmReportType ReportType {
			get {
				return reportType;
			}
			set {
				reportType = value;
			}
		}
		public string FileName {
			get {
				return fileName;
			}
			set {
				fileName = value;
			}
		}
		
		public string Path {
			get {
				return path;
			}
			set {
				path = value;
			}
		}
		
		#endregion
		
		#region DatabasePanel
		public string ConnectionString {
			get {
				return connectionString;
			}
			set {
				connectionString = value;
			}
		}
		public string CatalogName {
			get {
				return catalogName;
		}
			set {
				catalogName = value;
			}
		}
		
		public string SqlString {
			get {
				return sqlString;
			}
			set {
				sqlString = value;
			}
		}
		
		public CommandType CommandType 
		{
			get {
				return commandType;
			}
			set {
				commandType = value;
			}
		}
		
		public SharpReportCore.GlobalEnums.enmPushPullModel DataModel {
			get {
				return dataModel;
			}
			set {
				dataModel = value;
			}
		}
		

		
		public SharpQuerySchemaClassCollection Parameters {
			get {
				return queryParameters;
			}
			set {
				queryParameters = value;
			}
		}
		
		/// <summary>
		/// This Property is only usefull for ResultPanel
		/// </summary>
		public SharpQueryProcedure SharpQueryProcedure {
			get {
				return sharpQueryProcedure;
			}
			set {
				sharpQueryProcedure = value;
			}
		}
		
		#endregion
		
		
		#region PushModelPanel
		
		public ColumnCollection ColumnCollection {
			get {
				return columnCollection;
			}
			set {
				columnCollection = value;
			}
		}
		
		
		public ReportItemCollection ReportItemCollection {
			get {
				return reportItemCollection;
			}
			set {
				reportItemCollection = value;
			}
		}
		
		#endregion
		
	}
}
