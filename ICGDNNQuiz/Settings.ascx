<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="ICG.Modules.DnnQuiz.Settings" %>
<%@ Register tagPrefix="dnn" tagName="Label" src="~/Controls/LabelControl.ascx" %>

<div class="dnnForm">
    <div class="dnnFormItem">
        <dnn:Label id="lblDisallowEditAfterFirstQuiz" runat="server" suffix=":" controlName="chkDisallowEditAfterFirstQuiz" />
        <asp:CheckBox runat="server" ID="chkDisallowEditAfterFirstQuiz"/>
    </div>
</div>