<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditQuiz.ascx.cs" Inherits="ICG.Modules.DnnQuiz.EditQuiz" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<asp:HiddenField ID="hfQuizStart" runat="server" />
<asp:HiddenField ID="hfQuizEnd" runat="server" />

<div class="dnnForm">
    <asp:Literal runat="server" ID="litCannotEdit" Visible="False" />
    <asp:Label ID="lblGeneralSettings" runat="server" CssClass="Normal" resourcekey="lblGeneralSettings" />
    <div class="dnnFormItem">
        <dnn:Label ID="lblQuizTitle" runat="server" ControlName="txtQuizTitle" Suffix=":" cssclass="dnnFormRequired" />
        <asp:TextBox ID="txtQuizTitle" runat="server" CssClass="dnnFormRequired" MaxLength="1000" />
        <asp:RequiredFieldValidator ID="QuizTitleRequired" runat="server" Display="Dynamic" CssClass="dnnFormMessage dnnFormError" ControlToValidate="txtQuizTitle"
            resourcekey="QuizTitleRequired" />
    </div>
    <div class="dnnFormItem">
        <dnn:label ID="lblViewRole" runat="server" ControlName="ddlViewRole" Suffix=":"  DataTextField="RoleName" DataValueField="RoleName"/>
        <asp:DropDownList ID="ddlViewRole" runat="server" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblPassPercentage" runat="server" ControlName="ddlPassPercentage" Suffix=":" />
        <asp:DropDownList ID="ddlPassPercentage" runat="server">
            <asp:ListItem Text="100" Value="100" />
            <asp:ListItem Text="99" Value="99" />
            <asp:ListItem Text="98" Value="98" />
            <asp:ListItem Text="97" Value="97" />
            <asp:ListItem Text="96" Value="96" />
            <asp:ListItem Text="95" Value="95" />
            <asp:ListItem Text="94" Value="94" />
            <asp:ListItem Text="93" Value="93" />
            <asp:ListItem Text="92" Value="92" />
            <asp:ListItem Text="91" Value="91" />
            <asp:ListItem Text="90" Value="90" />
            <asp:ListItem Text="89" Value="89" />
            <asp:ListItem Text="88" Value="88" />
            <asp:ListItem Text="87" Value="87" />
            <asp:ListItem Text="86" Value="86" />
            <asp:ListItem Text="85" Value="85" />
            <asp:ListItem Text="84" Value="84" />
            <asp:ListItem Text="83" Value="83" />
            <asp:ListItem Text="82" Value="82" />
            <asp:ListItem Text="81" Value="81" />
            <asp:ListItem Text="80" Value="80" />
            <asp:ListItem Text="79" Value="79" />
            <asp:ListItem Text="78" Value="78" />
            <asp:ListItem Text="77" Value="77" />
            <asp:ListItem Text="76" Value="76" />
            <asp:ListItem Text="75" Value="75" />
            <asp:ListItem Text="74" Value="74" />
            <asp:ListItem Text="73" Value="73" />
            <asp:ListItem Text="72" Value="72" />
            <asp:ListItem Text="71" Value="71" />
            <asp:ListItem Text="70" Value="70" />
            <asp:ListItem Text="69" Value="69" />
            <asp:ListItem Text="68" Value="68" />
            <asp:ListItem Text="67" Value="67" />
            <asp:ListItem Text="66" Value="66" />
            <asp:ListItem Text="65" Value="65" />
            <asp:ListItem Text="64" Value="64" />
            <asp:ListItem Text="63" Value="63" />
            <asp:ListItem Text="62" Value="62" />
            <asp:ListItem Text="61" Value="61" />
            <asp:ListItem Text="60" Value="60" />
            <asp:ListItem Text="59" Value="59" />
            <asp:ListItem Text="58" Value="58" />
            <asp:ListItem Text="57" Value="57" />
            <asp:ListItem Text="56" Value="56" />
            <asp:ListItem Text="55" Value="55" />
            <asp:ListItem Text="54" Value="54" />
            <asp:ListItem Text="53" Value="53" />
            <asp:ListItem Text="52" Value="52" />
            <asp:ListItem Text="51" Value="51" />
            <asp:ListItem Text="50" Value="50" />
            <asp:ListItem Text="49" Value="49" />
            <asp:ListItem Text="48" Value="48" />
            <asp:ListItem Text="47" Value="47" />
            <asp:ListItem Text="46" Value="46" />
            <asp:ListItem Text="45" Value="45" />
            <asp:ListItem Text="44" Value="44" />
            <asp:ListItem Text="43" Value="43" />
            <asp:ListItem Text="42" Value="42" />
            <asp:ListItem Text="41" Value="41" />
            <asp:ListItem Text="40" Value="40" />
            <asp:ListItem Text="39" Value="39" />
            <asp:ListItem Text="38" Value="38" />
            <asp:ListItem Text="37" Value="37" />
            <asp:ListItem Text="36" Value="36" />
            <asp:ListItem Text="35" Value="35" />
            <asp:ListItem Text="34" Value="34" />
            <asp:ListItem Text="33" Value="33" />
            <asp:ListItem Text="32" Value="32" />
            <asp:ListItem Text="31" Value="31" />
            <asp:ListItem Text="30" Value="30" />
        </asp:DropDownList>
    </div>
    <div class="dnnFormItem">
        <dnn:label ID="lblAddRoleOnPass" runat="server" ControlName="ddlAddRole" Suffix=":" />
        <asp:DropDownList ID="ddlAddRole" runat="server" AppendDataBoundItems="true" DataTextField="RoleName" DataValueField="RoleName">
                <asp:ListItem Text="Do Not Add Role" Value="-1" />
            </asp:DropDownList>
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblExpires" runat="server" ControlName="chkExpires" Suffix=":" />
        <asp:CheckBox id="chkExpires" runat="server" AutoPostBack="true" 
                oncheckedchanged="chkExpires_CheckedChanged" />
    </div>
    <div class="dnnFormItem" id="divExpiration" runat="server">
        <dnn:label id="lblExpireTime" runat="server" controlName="txtExpireDuration" Suffix=":" cssclass="dnnFormRequired"/>
        <asp:TextBox ID="txtExpireDuration" runat="server" MaxLength="5" text="0" cssclass="dnnFormRequired" />
        <asp:RequiredFieldValidator ID="ExpireDurationRequired" runat="server" CssClass="dnnFormMessage dnnFormError" Display="Dynamic" ControlToValidate="txtExpireDuration" resourcekey="RequiredField" />
        <asp:CompareValidator ID="ExpireDurationFormat" runat="server" CssClass="dnnFormMessage dnnFormError" Display="Dynamic" ControlToValidate="txtExpireDuration" Operator="DataTypeCheck" Type="Integer" resourcekey="NumberField" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label id="lblAllowRetake" runat="server" controlname="chkAllowRetake" suffix=":" />
        <asp:checkbox ID="chkAllowRetake" runat="server" Checked="true" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblIsPublished" runat="server" ControlName="chkIsPublished" Suffix=":" />
        <asp:CheckBox ID="chkIsPublished" runat="server" />
    </div>
    <ul class="dnnActions">
        <li><asp:linkbutton cssclass="dnnPrimaryAction" id="btnSave" resourcekey="btnSave" runat="server" text="Update" OnClick="btnSave_Click"></asp:linkbutton></li>
        <li><asp:linkbutton cssclass="dnnSecondaryAction" id="btnCancel" resourcekey="btnCancel" runat="server" borderstyle="none" text="Cancel" causesvalidation="False" OnClick="btnCancel_Click" /></li>
    </ul>
</div>
<div class="dnnClear">&nbsp;</div>
<asp:Panel ID="pnlQuestions" runat="server" Visible="false">
    <table>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblQuestions" runat="server" CssClass="Normal" resourcekey="lblQuestions" />
            </td>
        </tr>
        <tr>
            <td width="400">
                <asp:ListBox ID="lstQuestions" runat="server" Width="400px" Height="250px"></asp:ListBox>
            </td>
            <td>
                <span class="Normal">Move Questions:</span><br />
                <asp:ImageButton ID="btnMoveUp" runat="server" ImageUrl="~/images/up.gif" tooltip="Move Up" AlternateText="Move Up" OnClick="btnMoveUp_Click" />
                <br />
                <asp:ImageButton ID="btnMoveDown" runat="server" ImageUrl="~/images/dn.gif" tooltip="Move Down" AlternateText="Move down" OnClick="btnMoveDown_Click" />
                <br />
                <br />
                <span class="Normal">Question Actions:</span><br />
                <asp:ImageButton ID="btnEditQuestion" runat="server" ImageUrl="~/images/edit.gif" ToolTip="Edit Question" AlternateText="Edit Question" OnClick="btnEditQuestion_Click" />
                <br />
                <asp:ImageButton ID="btnDeleteQuestion" runat="server" ImageUrl="~/images/delete.gif" ToolTip="Delete Question" AlternateText="Delete question" OnClick="btnDeleteQuestion_Click" />
                <br />
                <asp:ImageButton ID="btnAddQuestion" runat="server" ImageUrl="~/images/add.gif" ToolTip="Add Question" AlternateText="Add question" OnClick="btnAddQuestion_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>