﻿/*
 * Created by SharpDevelop.
 * User: Peter Forstmeier
 * Date: 07.07.2014
 * Time: 20:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ICSharpCode.SharpDevelop;
using Xceed.Wpf.Toolkit;
using ICSharpCode.Reporting.Addin.ReportWizard.Dialog;
using ICSharpCode.Reporting.Addin.ReportWizard.ViewModels;
using System.Linq;

namespace ICSharpCode.Reporting.Addin.ReportWizard.Dialog
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class ReportWizard : Window
	{
		readonly ReportWizardContext context;
		readonly BaseSettingsPage baseSettingsPage;
		
		public ReportWizard(ReportWizardContext context)
		{
			InitializeComponent();
			this.context = context;
			baseSettingsPage = new BaseSettingsPage();
		}
		
		
		void _wizard_Next(object sender, Xceed.Wpf.Toolkit.Core.CancelRoutedEventArgs e)
		{
			Console.WriteLine(" _wizard_Next {0}",((Wizard)sender).CurrentPage.Name);
		}
	
		
		void _wizard_PageChanged(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("_wizard_PageChanged {0}",((Wizard)sender).CurrentPage.Name);
		}
	
		void _wizard_Finish(object sender, RoutedEventArgs e)
		{
			foreach (WizardPage element in _wizard.Items) {
				var hc = element as IHasContext;
				if (hc != null) {
					UpdateContext(hc);
				}
			}
		}
  
		
		void UpdateContext(IHasContext hc)
		{
			
			switch (hc.ReportPageType) {
					case WizardPageType.BaseSettingsPage:{
						context.PageOneContext = hc.Context;
						break;
					}
					
					case WizardPageType.PushModelPage: {
						context.PushModelContext = hc.Context;
						break;
					}
			}
		}
	}
}