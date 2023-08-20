using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace product_management.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [Display (Name = "Tên sản phẩm")]
        public string Name { get; set; }

        [Display (Name = "Số lượng sản phẩm")]
        [Range (0, int.MaxValue, ErrorMessage = "Giá trị không hợp lệ")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Giá trị không hợp lệ")]
        public int Amount { get; set; }
        [Display (Name = "Giá bán sản phẩm ($)")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị không hợp lệ")]
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Giá trị không hợp lệ")]
        public double Price { get; set; }

        [Display (Name="Mô tả sản phẩm")]
        [Required(ErrorMessage = "Giá tiền là bắt buộc")]
        [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
        public string Description { get; set; }
    }
}