<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TakeQuiz.ascx.cs" Inherits="ICG.Modules.DnnQuiz.TakeQuiz" %>
<div style="text-align:left;">
<asp:Label ID="lblIntroText" runat="server" CssClass="Normal" resourcekey="lblIntroText" />
<asp:Label ID="lblStatus" runat="server" CssClass="Normal" />
<table width="100%">
    <tr>
        <td colspan="2">
            <asp:Label ID="lblChallengeResults" runat="server" CssClass="Normal" Visible="false" />
            <asp:Panel ID="pnlChallengeDisplay" runat="server">
                <asp:Repeater ID="rptChallengeQuestions" runat="server" OnItemDataBound="rptChallengeQuestions_ItemDataBound">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfQuestionId" runat="server" />
                        <asp:Literal ID="litPromptText" runat="server" Mode="PassThrough" />
                        <asp:RadioButtonList ID="rblQuestions" runat="server" CssClass="Normal" />
                        <asp:Literal ID="litQuestions" runat="server" Mode="PassThrough" />
                        <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" CssClass="Normal" Display="dynamic"
                            ControlToValidate="rblQuestions" Enabled="false" ValidationGroup="Quiz"/>
                    </ItemTemplate>
                    <SeparatorTemplate>
                        <asp:Literal ID="litSeparator" runat="server" Mode="PassThrough" />
                    </SeparatorTemplate>
                </asp:Repeater>
                
                <asp:Button id="btnSubmitAnswers" runat="server" CssClass="CommandButton" resourcekey="btnSubmitAnswers" ValidationGroup="Quiz" OnClick="btnSubmitAnswers_Click" />
                <asp:hyperlink ID="btnTakeAgain" runat="server" CssClass="CommandButton" resourcekey="btnTakeAgain" Visible="false" />
            </asp:Panel>
        </td>
    </tr>
</table>
</div>