<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditQuizQuestion.ascx.cs" Inherits="ICG.Modules.DnnQuiz.EditQuizQuestion" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>

<div class="dnnForm">
    <div class="dnnFormItem">
        <dnn:Label ID="lblPromptText" runat="server" ControlName="txtPromptText" suffix=":" cssclass="dnnFormRequired" />
        <dnn:TextEditor ID="txtPromptTextRich" runat="server"  HtmlEncode="false" />
        <asp:RequiredFieldValidator ID="PromptTextRequired" runat="server" CssClass="dnnFormMessage dnnFormError"
            Display="dynamic" ControlToValidate="txtPromptTextRich" resourcekey="PromptTextRequired" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblAnswer1" runat="server" ControlName="txtAnswer1" Suffix=":" cssclass="dnnFormRequired"/>
        <asp:TextBox ID="txtAnswer1" runat="server" TextMode="multiLine" MaxLength="500" CssClass="dnnFormRequired" />
        <asp:RequiredFieldValidator ID="Answer1Required" runat="server" CssClass="dnnFormMessage dnnFormError"
            Display="dynamic" ControlToValidate="txtAnswer1" resourcekey="Answer1Required" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblAnswer2" runat="server" ControlName="txtAnswer2" Suffix=":" />
        <asp:TextBox ID="txtAnswer2" runat="server" TextMode="multiLine" MaxLength="500" CssClass="dnnFormRequired"/>
        <asp:RequiredFieldValidator ID="Answer2Required" runat="server" CssClass="dnnFormMessage dnnFormError"
            Display="dynamic" ControlToValidate="txtAnswer2" resourcekey="Answer2Required" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblAnswer3" runat="server" ControlName="txtAnswer3" Suffix=":" />
        <asp:TextBox ID="txtAnswer3" runat="server" TextMode="multiLine" MaxLength="500" />
        <asp:Label ID="lblAnswer3Required" runat="server" Visible="false" CssClass="NormalRed" resourcekey="lblAnswer3Required" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblAnswer4" runat="server" ControlName="txtAnswer4" Suffix=":" />
        <asp:TextBox ID="txtAnswer4" runat="server" TextMode="multiLine" MaxLength="500" />
        <asp:Label ID="lblAnswer4Required" runat="server" Visible="false" CssClass="NormalRed" resourcekey="lblAnswer4Required" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblAnswer5" runat="server" ControlName="txtAnswer5" Suffix=":" />
        <asp:TextBox ID="txtAnswer5" runat="server" TextMode="multiLine" MaxLength="500" />
    </div>
    <div class="dnnFormItem">
        <dnn:Label ID="lblCorrectAnswer" runat="server" ControlName="ddlCorrectAnswer" Suffix=":" cssclass="dnnFormRequired" />
        <asp:DropDownList ID="ddlCorrectAnswer" runat="server">
            <asp:ListItem Text="Answer 1" Value="1" />
            <asp:ListItem Text="Answer 2" Value="2" />
            <asp:ListItem Text="Answer 3" Value="3" />
            <asp:ListItem Text="Answer 4" Value="4" />
            <asp:ListItem Text="Answer 5" Value="5" />
        </asp:DropDownList>
        <asp:Label ID="lblInvalidAnswer" runat="server" Visible="false" CssClass="NormalRed" resourcekey="lblInvalidAnswer" />
    </div>
    <ul class="dnnActions">
        <li><asp:linkbutton cssclass="dnnPrimaryAction" id="btnSave" resourcekey="btnSave" runat="server" borderstyle="none" text="Update" OnClick="btnSave_Click" /></li>
        <li><asp:linkbutton cssclass="dnnSecondaryAction" id="btnCancel" resourcekey="btnCancel" runat="server" borderstyle="none" text="Cancel" causesvalidation="False" OnClick="btnCancel_Click"/></li>
    </ul>
</div>

