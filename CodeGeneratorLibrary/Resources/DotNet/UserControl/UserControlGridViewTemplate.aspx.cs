<GenerateTag>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using <BLNameSpace>;
using <DTONameSpace>;
using System.Text.RegularExpressions;

/// <summary>
/// This class <ClassName>
/// </summary>
public partial class <ClassName> : <InheritsClass>
{
    #region Variable
    
    private DataTable dt;
    
    #endregion

    #region Constant
    #endregion

    #region Properties
    #endregion

    #region Page life circle

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        GridViewOutput.PageIndexChanging += GridViewOutput_PageIndexChanging;
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string nestedSystemMessage = string.Empty;
        string systemMessage = string.Empty;
        
        if (!Populate(out nestedSystemMessage))
        {
            ShowErrorMessage("{<GUIDPage_Load>}" + "  Error: Page_Load " +  nestedSystemMessage);
            return;
        }
            
        GridViewOutput.DataSource = dt;
        GridViewOutput.DataBind();
    }

    /// <summary>
    /// Raises the <see cref="E:PreRender"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Populates the specified data.
    /// </summary>
    /// <param name="<ParamDesc>">The <ParamDesc> DTO list.</param>
    /// <param name="systemMessage">The systemMessage.</param>
    /// <returns>
    /// return true if success
    /// </returns>
    private bool Populate(out string systemMessage)
    {
        string nestedSystemMessage = string.Empty;
        systemMessage = string.Empty;

        if (!<BLClass>.Instance.<Method>(out dt, out nestedSystemMessage))
        {
            systemMessage = "{<GUIDPopulate>}" + "  Error: Populate " +  nestedSystemMessage;
            return false;
        }
        
        GridViewOutput.DataSource = dt;
        GridViewOutput.DataBind();
        return true;
    }

    #endregion

    #region Events

    /// <summary>
    /// Handles the PageIndexChanging event of the GridViewOutput control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="GridViewPageEventArgs"/> instance containing the event data.</param>
    protected void GridViewOutput_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string nestedSystemMessage = string.Empty;
        GridViewOutput.PageIndex = e.NewPageIndex;
        
        if (!Populate(out nestedSystemMessage))
        {
            ShowErrorMessage("{<GUIDGridViewOutput_PageIndexChanging>}" + "  Error:  GridViewOutput_PageIndexChanging " +  nestedSystemMessage);
            return;
        }
    }

    /// <summary>
    /// Handles the RowCommand event of the GridViewOutput control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
    protected void GridViewOutput_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName.Equals("detail"))
        {
            <DataTypeVariable> = <ParseDataType>GridViewOutput.DataKeys[index].Value.ToString());
            IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                         where i.Field<DataType>(<FieldID>).Equals(parseID)
                                         select i;
            DataTable detailTable = query.CopyToDataTable<DataRow>();
            DetailsView1.DataSource = detailTable;
            DetailsView1.DataBind();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#detailModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DetailModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("editRecord"))
        {
            GridViewRow gvrow = GridViewOutput.Rows[index];
            <HtmlDecodeRows>                
            lblResult.Visible = false;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#editModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditModalScript", sb.ToString(), false);
        }
        else if (e.CommandName.Equals("deleteRecord"))
        {
            string code = GridViewOutput.DataKeys[index].Value.ToString();
            hfCode.Value = code;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#deleteModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
        }
    }
    
    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string nestedSystemMessage = string.Empty;
        <SaveEdit>
        <DtoInitUpdate>
        if (!Populate(out nestedSystemMessage))
        {
            ShowErrorMessage("{<GUIDbtnSave_Click>}" + "  Error:  Populate " +  nestedSystemMessage);
            return;
        }
            
        StringBuilder sb = new StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#editModal').modal('hide');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "EditHideModalScript", sb.ToString(), false);

    }
    
    /// <summary>
    /// Executes the update.
    /// </summary>
    /// <param name="<dtoClass>">The <dtoClass>.</param>
    private bool executeUpdate(<DtoClass> <DtoClassTemp>, out string systemMessage)
    {
        string nestedSystemMessage = string.Empty;
        systemMessage = string.Empty;
        if (!<BLClass>.Instance.<UpdateMethod>(<DtoClassTemp>, out nestedSystemMessage))
        {
            systemMessage ="{<GUIDExecuteUpdate>}" + "  Error:  executeUpdate " + nestedSystemMessage;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#addModal').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddShowModalScript", sb.ToString(), false);
    }

    /// <summary>
    /// Handles the Click event of the btnAddRecord control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
     protected void btnAddRecord_Click(object sender, EventArgs e)
     {
        string nestedSystemMessage = string.Empty;        
        <SaveAdd>
        <DtoInitAdd>
        if (!Populate(out nestedSystemMessage))
        {
            ShowErrorMessage("{<GUIDbtnAddRecord_Click>}" + "  Error:  Populate " +  nestedSystemMessage);
            return;
        }
         StringBuilder sb = new StringBuilder();
         sb.Append(@"<script type='text/javascript'>");
         sb.Append("$('#addModal').modal('hide');");
         sb.Append(@"</script>");
         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddHideModalScript", sb.ToString(), false);
     }

    /// <summary>
    /// Executes the add.
    /// </summary>
     private bool executeAdd(<DtoClass> <DtoClassTemp>, out string systemMessage)
     {
        string nestedSystemMessage = string.Empty;
        systemMessage = string.Empty;  
        if (!<BLClass>.Instance.<AddMethod>(<DtoClassTemp>, out nestedSystemMessage))
        {
            systemMessage ="{<GUIDExecuteAdd>}" + "  Error:  executeAdd " + nestedSystemMessage;
            return false;
        }

        return true;
     }

    /// <summary>
    /// Handles the Click event of the btnDelete control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
     protected void btnDelete_Click(object sender, EventArgs e)
     {
        string nestedSystemMessage = string.Empty;
        <DeleteAdd>
        <DtoInitDeleteAdd>
        if (!Populate(out nestedSystemMessage))
        {
            ShowErrorMessage("{<GUIDbtnDelete_Click>}" + "  Error:  Populate " +  nestedSystemMessage);
            return;
        }
         StringBuilder sb = new StringBuilder();
         sb.Append(@"<script type='text/javascript'>");
         ///sb.Append("alert('Record deleted Successfully');");
         sb.Append("$('#deleteModal').modal('hide');");
         sb.Append(@"</script>");
         ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "delHideModalScript", sb.ToString(), false);
     }

    /// <summary>
    /// Executes the delete.
    /// </summary>
    /// <param name="code">The code.</param>
    private bool executeDelete(<DtoClass> <DtoClassTemp>, out string systemMessage)
    {
        string nestedSystemMessage = string.Empty;
        systemMessage = string.Empty;
        if (!<BLClass>.Instance.<DeleteMethod>(<DtoClassTemp>, out nestedSystemMessage))
        {
            systemMessage ="{<GUIDExecuteDelete>}" + "  Error:  executeDelete " + nestedSystemMessage;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Shows the error message.
    /// </summary>
    private void ShowErrorMessage(string errorMessage)
    {
        ////btnAdd.Visible = false;
        errorLabel.Text = errorMessage.Replace("\n", "\\n").Replace("\r", "").Replace("'", "\\'");
        StringBuilder sb = new StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#errorModal').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ErrorModalScript", sb.ToString(), false);
    }

    #endregion
}