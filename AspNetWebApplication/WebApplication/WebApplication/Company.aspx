<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="WebApplication.Company" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="hfID" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="CompanyName"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="DepartmentName"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDepartmentName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="DepartmentLocation"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDepartmentLocation" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="EmployeeName"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtEmployeeName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvCompany" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" />
                    <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" />
                    <asp:BoundField DataField="DepartmentLocation" HeaderText="DepartmentLocation" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="lnk_OnClick">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
