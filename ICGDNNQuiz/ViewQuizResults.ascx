<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewQuizResults.ascx.cs" Inherits="ICG.Modules.DnnQuiz.ViewQuizResults" %>

<div style="text-align: left;" class="Normal" >
    <asp:literal ID="litHeader" runat="server" Mode="PassThrough" />
    <asp:DataGrid ID="dgrQuizResults" runat="server" AutoGenerateColumns="false" CssClass="dnnGrid"
        CellPadding="5">
        <ItemStyle CssClass="dnnGridItem"/>
        <AlternatingItemStyle CssClass="dnnGridAltItem" />
        <HeaderStyle CssClass="dnnGridHeader" />
        <Columns>
            <asp:BoundColumn DataField="DateTaken" HeaderText="DateTaken"/>
            <asp:BoundColumn DataField="DisplayName" HeaderText="DisplayName" />
            <asp:BoundColumn DataField="NumberCorrect" HeaderText="NumberCorrect" />
            <asp:BoundColumn DataField="NumberIncorrect" HeaderText="NumberIncorrect" />
            <asp:BoundColumn DataField="PercentageDisplay" HeaderText="Percentage" />
            <asp:BoundColumn DataField="Passed" HeaderText="Passed" />
        </Columns>
    </asp:DataGrid>
</div>