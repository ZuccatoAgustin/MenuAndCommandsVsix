/***************************************************************************
 
Copyright (c) Microsoft Corporation. All rights reserved.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Samples.VisualStudio.MenuCommands
{
    /// <summary>
    /// This is the class that implements the package. This is the class that Visual Studio will create
    /// when one of the commands will be selected by the user, and so it can be considered the main
    /// entry point for the integration with the IDE.
    /// Notice that this implementation derives from Microsoft.VisualStudio.Shell.Package that is the
    /// basic implementation of a package provided by the Managed Package Framework (MPF).
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]

    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidsList.guidMenuAndCommandsPkg_string)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ComVisible(true)]
    public sealed class MenuCommandsPackage : Package
    {
        #region Member Variables
        private OleMenuCommand dynamicVisibilityCommand1;
        private OleMenuCommand dynamicVisibilityCommand2;
        #endregion

        /// <summary>
        /// Default constructor of the package. This is the constructor that will be used by VS
        /// to create an instance of your package. Inside the constructor you should do only the
        /// more basic initializazion like setting the initial value for some member variable. But
        /// you should never try to use any VS service because this object is not part of VS
        /// environment yet; you should wait and perform this kind of initialization inside the
        /// Initialize method.
        /// </summary>
        public MenuCommandsPackage()
        {
        }

        /// <summary>
        /// Initialization of the package; this is the place where you can put all the initialization
        /// code that relies on services provided by Visual Studio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Now get the OleCommandService object provided by the MPF; this object is the one
            // responsible for handling the collection of commands implemented by the package.
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Now create one object derived from MenuCommand for each command defined in
                // the VSCT file and add it to the command service.

                // For each command we have to define its id that is a unique Guid/integer pair.
                CommandID id = new CommandID(GuidsList.guidMenuAndCommandsCmdSet, PkgCmdIDList.cmdidMyCommand);
                // Now create the OleMenuCommand object for this command. The EventHandler object is the
                // function that will be called when the user will select the command.
                OleMenuCommand command = new OleMenuCommand(new EventHandler(MenuCommandCallback), id);
                // Add the command to the command service.
                mcs.AddCommand(command); 
            }
        }

        #region Commands Actions
 

        /// <summary>
        /// Event handler called when the user selects the Sample command.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Microsoft.Samples.VisualStudio.MenuCommands.MenuCommandsPackage.OutputCommandString(System.String)")]
        private void MenuCommandCallback(object caller, EventArgs args)
        {
              
            EnvDTE.DTE _applicationObject = (EnvDTE.DTE)GetService(typeof(SDTE));
          
            TextSelection textSelection = this.GetTextSelection(_applicationObject);

            var cl = new CreateLabel.CreateLabelForm();
            cl.textSelection = textSelection;

            cl.FileSelection = GetFileSelection(_applicationObject);

            cl.Show();
            
        }


        private TextSelection GetTextSelection(DTE passedDTE)
        {
            if (passedDTE.ActiveDocument != null)
            {
                ProjectItem projectItem = passedDTE.ActiveDocument.ProjectItem;
                FileCodeModel fileCodeModel = projectItem.FileCodeModel;

                return (TextSelection)passedDTE.ActiveDocument.Selection;
            }
            return null;
        }

        private string GetFileSelection(DTE passedDTE)
        {
            if (passedDTE.ActiveDocument != null && passedDTE.ActiveDocument.ActiveWindow != null)
            {
                return passedDTE.ActiveDocument.ActiveWindow.Caption;
            }
            return null;
        }

        /// <summary>

        #endregion
    }
}
