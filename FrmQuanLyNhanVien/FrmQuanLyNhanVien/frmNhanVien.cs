using System;
using System.Data;
using System.Globalization; // For CultureInfo.InvariantCulture
using System.Windows.Forms;
// using System.Data.SqlClient; // Cần thiết nếu bạn tự xử lý Parameterized Queries

namespace FrmQuanLyNhanVien
{
    public partial class frmNhanVien : Form
    {
        bool isAddingNew = false; // Thay thế cho AddNew, rõ ràng hơn

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadGridData();
            SetBrowseMode(); // Chuyển sang chế độ xem ban đầu
        }

        #region UI State Management
        private void SetControlEnabledState(bool isEnabled)
        {
            // Không bao gồm txtMaNv và txtNameLogin ở đây vì chúng có logic riêng
            txtTenNv.Enabled = isEnabled;
            txtDiaChi.Enabled = isEnabled;
            txtDienThoai.Enabled = isEnabled;
            dtpNgaySinh.Enabled = isEnabled;
            dtpNgayVaoLam.Enabled = isEnabled;
            rbtnNam.Enabled = isEnabled;
            rbtnNu.Enabled = isEnabled;
            txtLuong.Enabled = isEnabled;
            // txtNameLogin (TenTaiKhoan hiện tại) thường không cho sửa
            // txtNameTk (TenTaiKhoan mới), txtPassword (Mật khẩu mới) chỉ bật khi thêm mới
        }

        private void SetBrowseMode()
        {
            isAddingNew = false;
            SetControlEnabledState(false); // Các trường thông tin chính không cho sửa trực tiếp khi duyệt

            txtMaNv.Enabled = false;       // Không cho sửa mã khi duyệt
            txtNameLogin.Enabled = false;  // Tên tài khoản hiện tại chỉ hiển thị
            txtNameTk.Enabled = false;     // Tên tài khoản mới không dùng khi duyệt
            txtPassword.Enabled = false;   // Mật khẩu mới không dùng khi duyệt

            btnAddNew.Enabled = true;
            btnSave.Enabled = false;
            btnCancer.Enabled = false;
            btnUpdate.Enabled = dgvDanhSachNhanVien.SelectedRows.Count > 0; // Bật nếu có dòng được chọn
            btnDelete.Enabled = dgvDanhSachNhanVien.SelectedRows.Count > 0; // Bật nếu có dòng được chọn
            btnClose.Enabled = true;
            dgvDanhSachNhanVien.Enabled = true;

            ClearAccountCreationFields(); // Xóa trường tạo tài khoản
        }

        private void SetAddingMode()
        {
            isAddingNew = true;
            ClearInputFields();
            SetControlEnabledState(true);

            txtMaNv.Enabled = true;        // Cho phép nhập Mã NV khi thêm mới
            txtNameLogin.Enabled = false;   // Tên tài khoản hiện tại không dùng
            txtNameTk.Enabled = true;      // Cho phép nhập tên tài khoản mới
            txtPassword.Enabled = true;    // Cho phép nhập mật khẩu mới

            btnAddNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancer.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnClose.Enabled = true; // Hoặc false tùy ý
            dgvDanhSachNhanVien.Enabled = false;

            txtMaNv.Focus();
        }

        private void SetEditingMode()
        {
            isAddingNew = false; // Đang sửa, không phải thêm mới
            SetControlEnabledState(true);

            txtMaNv.Enabled = false;       // Không cho sửa Mã NV (Khóa chính)
            txtNameLogin.Enabled = false;  // Không cho sửa Tên tài khoản hiện tại qua form này
            txtNameTk.Enabled = false;     // Tên tài khoản mới không dùng khi sửa NV
            txtPassword.Enabled = false;   // Mật khẩu mới không dùng khi sửa NV (trừ khi có logic đổi MK)

            btnAddNew.Enabled = false;
            btnSave.Enabled = true;
            btnCancer.Enabled = true;
            btnUpdate.Enabled = false; // Vô hiệu hóa chính nó
            btnDelete.Enabled = false;
            btnClose.Enabled = true; // Hoặc false tùy ý
            dgvDanhSachNhanVien.Enabled = false;

            txtTenNv.Focus();
        }

        private void ClearInputFields()
        {
            txtMaNv.Clear();
            txtTenNv.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            dtpNgaySinh.Checked = false; // Nếu ShowCheckBox = true, thể hiện là chưa chọn
            dtpNgayVaoLam.Value = DateTime.Now;
            dtpNgayVaoLam.Checked = false; // Nếu ShowCheckBox = true
            rbtnNam.Checked = true;
            txtLuong.Clear();
            txtNameLogin.Clear(); // Xóa tên tài khoản đang hiển thị
            ClearAccountCreationFields();
        }

        private void ClearAccountCreationFields()
        {
            txtNameTk.Clear();
            txtPassword.Clear();
        }
        #endregion

        private void LoadGridData()
        {
            DBServices db = new DBServices();
            // Nên SELECT cụ thể các cột thay vì dùng *
            // Và JOIN với TaiKhoan để lấy VaiTro nếu cần hiển thị
            string sSql = "SELECT MaNv, TenNv, NgaySinh, GioiTinh, DiaChi, DienThoai, NgayVaoLam, Luong, TenTaiKhoan FROM NhanVien";
            dgvDanhSachNhanVien.DataSource = db.GetData(sSql);
            // Sau khi load, không có dòng nào được chọn thì nút Sửa/Xóa nên tắt
            if (dgvDanhSachNhanVien.SelectedRows.Count == 0)
            {
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void dgvDanhSachNhanVien_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDanhSachNhanVien.Rows.Count)
            {
                DataGridViewRow row = dgvDanhSachNhanVien.Rows[e.RowIndex];

                string GetCellValueSafe(string columnName)
                {
                    var cell = row.Cells[columnName];
                    return cell != null && cell.Value != null && cell.Value != DBNull.Value ? cell.Value.ToString() : string.Empty;
                }

                txtMaNv.Text = GetCellValueSafe("MaNv");
                txtTenNv.Text = GetCellValueSafe("TenNv");

                string ngaySinhStr = GetCellValueSafe("NgaySinh");
                if (!string.IsNullOrEmpty(ngaySinhStr) && DateTime.TryParse(ngaySinhStr, out DateTime ngaySinh))
                {
                    dtpNgaySinh.Value = ngaySinh;
                    dtpNgaySinh.Checked = true;
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now; // Hoặc MinDate
                    dtpNgaySinh.Checked = false;
                }

                string gioiTinh = GetCellValueSafe("GioiTinh");
                if (gioiTinh.Equals("Nam", StringComparison.OrdinalIgnoreCase))
                    rbtnNam.Checked = true;
                else if (gioiTinh.Equals("Nữ", StringComparison.OrdinalIgnoreCase) || gioiTinh.Equals("Nu", StringComparison.OrdinalIgnoreCase))
                    rbtnNu.Checked = true;
                else
                {
                    rbtnNam.Checked = false; // Hoặc một giá trị mặc định
                    rbtnNu.Checked = false;
                }

                txtDiaChi.Text = GetCellValueSafe("DiaChi");
                txtDienThoai.Text = GetCellValueSafe("DienThoai");

                string ngayVaoLamStr = GetCellValueSafe("NgayVaoLam");
                if (!string.IsNullOrEmpty(ngayVaoLamStr) && DateTime.TryParse(ngayVaoLamStr, out DateTime ngayVaoLam))
                {
                    dtpNgayVaoLam.Value = ngayVaoLam;
                    dtpNgayVaoLam.Checked = true;
                }
                else
                {
                    dtpNgayVaoLam.Value = DateTime.Now; // Hoặc MinDate
                    dtpNgayVaoLam.Checked = false;
                }

                txtLuong.Text = GetCellValueSafe("Luong");
                txtNameLogin.Text = GetCellValueSafe("TenTaiKhoan"); // Hiển thị TenTaiKhoan hiện tại

                // Nếu không đang ở chế độ thêm/sửa (tức là đang duyệt) thì bật nút Sửa/Xóa
                if (!isAddingNew && !btnSave.Enabled) // btnSave.Enabled là true khi đang Add/Edit
                {
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
                ClearAccountCreationFields(); // Xóa trường tạo tài khoản khi chỉ chọn xem
            }
        }

        private void dgvDanhSachNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            // Khi lựa chọn thay đổi và không ở chế độ thêm/sửa
            if (!isAddingNew && !btnSave.Enabled)
            {
                bool rowSelected = dgvDanhSachNhanVien.SelectedRows.Count > 0;
                btnUpdate.Enabled = rowSelected;
                btnDelete.Enabled = rowSelected;
                if (!rowSelected)
                { // Nếu không có dòng nào được chọn, xóa thông tin trên form
                    ClearInputFields();
                }
            }
        }


        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SetAddingMode();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachNhanVien.SelectedRows.Count > 0)
            {
                SetEditingMode();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancer_Click(object sender, EventArgs e)
        {
            // Nếu hủy khi đang sửa, load lại dữ liệu của dòng đang chọn
            if (!isAddingNew && dgvDanhSachNhanVien.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dgvDanhSachNhanVien.SelectedRows[0].Index;
                if (selectedRowIndex >= 0)
                {
                    // Corrected the instantiation of DataGridViewCellMouseEventArgs
                    dgvDanhSachNhanVien_CellMouseClick(dgvDanhSachNhanVien, new DataGridViewCellMouseEventArgs(0, selectedRowIndex, 0, 0, null));
                }
            }
            else // Nếu hủy khi đang thêm hoặc không có dòng nào chọn
            {
                ClearInputFields();
            }
            SetBrowseMode();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool TaiKhoanTonTai(string tenTaiKhoan)
        {
            // Quan trọng: Sử dụng Parameterized Query ở đây
            // string sql = "SELECT COUNT(*) FROM TaiKhoan WHERE TenTaiKhoan = @TenTK";
            // SqlParameter[] parameters = { new SqlParameter("@TenTK", tenTaiKhoan) };
            // DataTable resultTable = db.GetDataWithParams(sql, parameters); // Giả sử có hàm này
            string sql = string.Format("SELECT COUNT(*) FROM TaiKhoan WHERE TenTaiKhoan = N'{0}'", tenTaiKhoan); // Tạm thời
            DBServices db = new DBServices();
            DataTable resultTable = db.GetData(sql);

            if (resultTable != null && resultTable.Rows.Count > 0 && resultTable.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToInt32(resultTable.Rows[0][0]) > 0;
            }
            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // --- Lấy dữ liệu chung từ Form ---
            string maNv = txtMaNv.Text.Trim();
            string tenNv = txtTenNv.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string dienThoai = txtDienThoai.Text.Trim();

            // Xử lý ngày tháng (kiểm tra dtp.Checked nếu ShowCheckBox=true)
            // Nếu dtpNgaySinh.Checked là false, nghĩa là người dùng không muốn nhập/để null
            object ngaySinhDbValue = dtpNgaySinh.Checked ? (object)dtpNgaySinh.Value.ToString("yyyy-MM-dd") : DBNull.Value;
            object ngayVaoLamDbValue = dtpNgayVaoLam.Checked ? (object)dtpNgayVaoLam.Value.ToString("yyyy-MM-dd") : DBNull.Value;

            string gioiTinh = rbtnNam.Checked ? "Nam" : "Nữ";

            string luongStr = txtLuong.Text.Trim().Replace(',', '.');
            decimal luongDecimal = 0; // Initialize luongDecimal to avoid CS0165 error
            if (!string.IsNullOrEmpty(luongStr) && !decimal.TryParse(luongStr, NumberStyles.Any, CultureInfo.InvariantCulture, out luongDecimal))
            {
                MessageBox.Show("Vui lòng nhập giá trị số hợp lệ cho Lương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuong.Focus();
                return;
            }
            object luongDbValue = string.IsNullOrEmpty(luongStr) ? DBNull.Value : (object)luongDecimal;

            DBServices db = new DBServices();
            // *** NÊN BẮT ĐẦU TRANSACTION Ở ĐÂY ***
            // db.BeginTransaction(); 

            try
            {
                if (isAddingNew)
                {
                    // --- THÊM MỚI NHÂN VIÊN ---
                    if (string.IsNullOrEmpty(maNv))
                    {
                        MessageBox.Show("Mã nhân viên không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaNv.Focus();
                        return;
                        // db.RollbackTransaction(); // Nếu có transaction
                    }
                    // Kiểm tra trùng Mã NV nếu cần (MaNv là PK, CSDL sẽ báo lỗi nếu trùng)

                    string newTenTaiKhoan = txtNameTk.Text.Trim();
                    string newMatKhau = txtPassword.Text;
                    string newVaiTro = "NhanVien"; // Hoặc lấy từ ComboBox/quy tắc nào đó

                    if (string.IsNullOrEmpty(newTenTaiKhoan) || string.IsNullOrEmpty(newMatKhau))
                    {
                        MessageBox.Show("Khi thêm nhân viên mới, Tên tài khoản và Mật khẩu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // db.RollbackTransaction();
                        return;
                    }

                    if (TaiKhoanTonTai(newTenTaiKhoan))
                    {
                        MessageBox.Show($"Tên tài khoản '{newTenTaiKhoan}' đã tồn tại. Vui lòng chọn tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // db.RollbackTransaction();
                        return;
                    }

                    // 1. Thêm TaiKhoan
                    string sqlInsertTaiKhoan = string.Format("INSERT INTO TaiKhoan (TenTaiKhoan, MatKhau, VaiTro) VALUES (N'{0}', N'{1}', N'{2}')", newTenTaiKhoan, newMatKhau, newVaiTro);
                    db.runQuery(sqlInsertTaiKhoan);

                    // 2. Thêm NhanVien
                    string sqlInsertNhanVien = string.Format(CultureInfo.InvariantCulture, // Để xử lý số thập phân đúng cách
                        "INSERT INTO NhanVien (MaNv, TenNv, DiaChi, DienThoai, NgaySinh, NgayVaoLam, GioiTinh, Luong, TenTaiKhoan) " +
                        "VALUES (N'{0}', N'{1}', N'{2}', N'{3}', {4}, {5}, N'{6}', {7}, N'{8}')",
                        maNv, tenNv, diaChi, dienThoai,
                        ngaySinhDbValue == DBNull.Value ? "NULL" : $"'{((DateTime)ngaySinhDbValue).ToString("yyyy-MM-dd")}'", //Sửa chỗ này theo định dạng ngày
                        ngayVaoLamDbValue == DBNull.Value ? "NULL" : $"'{((DateTime)ngayVaoLamDbValue).ToString("yyyy-MM-dd")}'", //Sửa chỗ này
                        gioiTinh,
                        luongDbValue == DBNull.Value ? "NULL" : luongDbValue.ToString(),
                        newTenTaiKhoan);
                    db.runQuery(sqlInsertNhanVien);

                    MessageBox.Show("Thêm nhân viên và tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // --- CẬP NHẬT NHÂN VIÊN ---
                {
                    if (string.IsNullOrEmpty(maNv)) // MaNv được lấy từ txtMaNv (đã disable, giữ giá trị cũ)
                    {
                        MessageBox.Show("Không xác định được nhân viên để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // db.RollbackTransaction();
                        return;
                    }
                    string sqlUpdateNhanVien = string.Format(CultureInfo.InvariantCulture,
                       "UPDATE NhanVien SET TenNv=N'{0}', DiaChi=N'{1}', DienThoai=N'{2}', " +
                       "NgaySinh={3}, NgayVaoLam={4}, GioiTinh=N'{5}', Luong={6} " +
                       "WHERE MaNv=N'{7}'",
                       tenNv, diaChi, dienThoai,
                       ngaySinhDbValue == DBNull.Value ? "NULL" : $"'{((DateTime)ngaySinhDbValue).ToString("yyyy-MM-dd")}'",
                       ngayVaoLamDbValue == DBNull.Value ? "NULL" : $"'{((DateTime)ngayVaoLamDbValue).ToString("yyyy-MM-dd")}'",
                       gioiTinh,
                       luongDbValue == DBNull.Value ? "NULL" : luongDbValue.ToString(),
                       maNv);
                    db.runQuery(sqlUpdateNhanVien);

                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadGridData();
                SetBrowseMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}\nChi tiết: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachNhanVien.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này và tài khoản liên kết (nếu có)?\nHành động này không thể hoàn tác.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string maNvToDelete = txtMaNv.Text; // Lấy từ form (đã disable, giữ giá trị đang chọn)
                    string tenTaiKhoanToDelete = txtNameLogin.Text; // Lấy từ form

                    if (string.IsNullOrEmpty(maNvToDelete))
                    {
                        MessageBox.Show("Không thể xác định mã nhân viên để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DBServices db = new DBServices();
                    // *** NÊN BẮT ĐẦU TRANSACTION Ở ĐÂY ***
                    try
                    {
                        // 1. Xóa NhanVien trước
                        // Quan trọng: Sử dụng Parameterized Query
                        // string sqlDeleteNhanVien = "DELETE FROM NhanVien WHERE MaNv = @MaNv";
                        // db.ExecuteNonQueryWithParams(sqlDeleteNhanVien, new SqlParameter("@MaNv", maNvToDelete));
                        string sqlDeleteNhanVien = string.Format("DELETE FROM NhanVien WHERE MaNv = N'{0}'", maNvToDelete);
                        db.runQuery(sqlDeleteNhanVien);

                        // 2. Xóa TaiKhoan nếu có và không còn NhanVien nào khác tham chiếu (trong trường hợp này thường là 1-1)
                        if (!string.IsNullOrEmpty(tenTaiKhoanToDelete))
                        {
                            // Kiểm tra xem có NhanVien nào khác còn dùng TaiKhoan này không
                            // string checkSql = "SELECT COUNT(*) FROM NhanVien WHERE TenTaiKhoan = @TenTK";
                            // int count = (int)db.ExecuteScalarWithParams(checkSql, new SqlParameter("@TenTK", tenTaiKhoanToDelete));
                            // if (count == 0) {
                            //    string sqlDeleteTaiKhoan = "DELETE FROM TaiKhoan WHERE TenTaiKhoan = @TenTK";
                            //    db.ExecuteNonQueryWithParams(sqlDeleteTaiKhoan, new SqlParameter("@TenTK", tenTaiKhoanToDelete));
                            // }
                            // Tạm thời, giả sử nếu xóa NV thì xóa luôn TK liên kết nếu có tên
                            string sqlDeleteTaiKhoan = string.Format("DELETE FROM TaiKhoan WHERE TenTaiKhoan = N'{0}'", tenTaiKhoanToDelete);
                            db.runQuery(sqlDeleteTaiKhoan); // Có thể lỗi nếu TK này vẫn được tham chiếu bởi NV khác (ít khả năng với thiết kế này)
                        }

                        // db.CommitTransaction();
                        LoadGridData();
                        ClearInputFields(); // Xóa thông tin trên form sau khi xóa
                        SetBrowseMode();  // Đặt lại UI
                        MessageBox.Show("Xóa nhân viên (và tài khoản nếu có) thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // db.RollbackTransaction();
                        MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}