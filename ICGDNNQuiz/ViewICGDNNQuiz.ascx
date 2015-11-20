<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewICGDNNQuiz.ascx.cs" Inherits="ICG.Modules.DnnQuiz.ViewICGDNNQuiz" %>

<asp:Repeater ID="rptQuizList" runat="server" OnItemDataBound="rptQuizList_ItemDataBound">
    <HeaderTemplate>
        <asp:Label ID="lblHeader" runat="server" resourcekey="HeaderTemplate" />
    </HeaderTemplate>
    <ItemTemplate>
        <asp:Literal ID="litItem" runat="server" Mode="passthrough" />
    </ItemTemplate>
    <FooterTemplate>
        <asp:Label ID="lblFooter" runat="server" resourcekey="FooterTemplate" />
    </FooterTemplate>
</asp:Repeater>

<asp:Label ID="lblNoQuizzes" runat="server" CssClass="Normal" resourcekey="lblNoQuizzes" />
<asp:Label ID="lblMustLogin" runat="server" CssClass="Normal" resourcekey="lblMustLogin" />