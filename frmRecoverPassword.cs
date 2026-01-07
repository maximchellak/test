#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using Gizmox.WebGUI.Common;
using Gizmox.WebGUI.Forms;
using StarNet.Core;
using StarNet.Forms.Core;
using StarNet.Model.Admin;
using StarNet.ResourceFiles;
using StarNet.Common;
using StarNet.Controls.UI;
using StarNet.Model.Enums;

#endregion

namespace StarNet.Forms.Admin
{
    public partial class frmRecoverPassword : BaseForm
    {
        private User _user = null;

        public frmRecoverPassword()
        {
            InitializeComponent();
            InitializeEx();

            Render();
        }

        private void InitializeEx()
        {
        }

        public override object GetEntity(int? id)
        {
            return RequestContext.BusinessLayer.AdminBl.GetUser(id.Value);
        }

        public override IEditFormBinder CreateBinder()
        {
            var editFormBinder = new EditFormBinder<User>();

            editFormBinder
                .Id(x => x.Id)
                .IsNewEntityMethod(x => x.Id == 0)

                .MapMandatoryValidation(txtUsername)
            ;

            return editFormBinder;
        }

        public override bool Save()
        {
            SetMessage(null);

            var value = txtUsername.Text;

            _user = RequestContext.BusinessLayer.AdminBl.GetUserByUserNameOrEmail(value);

            if (_user != null)
            {
                if (string.IsNullOrEmpty(_user.Email))
                {
                    SetMessage(General.UserEmailNotSet, true);
                }
                else
                {
                    var response = SecurityServices.ChangePassword(_user.Id, null, null);

                    if (response.IsSuccessfull)
                    {
                        var result = SendEmail(_user, response.NewPassword);

                        if (string.IsNullOrEmpty(result))
                        {
                            SetMessage(General.NewPasswordWillBeSendToEmail);

                            btnSave.Enabled = false;

                            return true;
                        }

                        SetMessage(result, true);
                    }
                    else
                    {
                        SetMessage(General.PasswordRecoveryError, true);
                    }
                }

                return false;
            }

            SetMessage(General.UnknownUserNameOrEmail, true);

            return false;
        }

        public override bool UseLockManager
        {
            get
            {
                return false;
            }
        }

        public override bool CloseAfterSave
        {
            get
            {
                return false;
            }
        }

        private void SetMessage(string text, bool isError = false)
        {
            lblMessage.Text = text;

            lblMessage.SetFontStyle(FontStyle.Bold);

            if (isError)
            {
                lblMessage.ForeColor = Color.Red;
            }
            else
            {
                lblMessage.ForeColor = Color.DarkBlue;
            }
        }

        private string SendEmail(User user, string newPassword)
        {
            var fields = new Dictionary<string, string>(1);
            fields.Add(EmailField.Password, newPassword);

            var result = StarNet.Core.Utils.SendGeneralEmail(
                EmailTypeEnum.RecoverPassword.ToInt(),
                new string[] { user.Email.Trim() },
                fields);

            return result;
        }
    }
}