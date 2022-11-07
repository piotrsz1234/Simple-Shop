using Terminal.Gui;

namespace TextInterface.UI
{
    public class ProductAddEditFrame : FrameView
    {
        private TextView txtName;
        private TextView txtBarcode;
        private CheckBox cbxAgeRestricted;
        private CheckBox cbxCountable;
        
        public ProductAddEditFrame()
        {
            SetLabels();
        }

        private void SetLabels()
        {
            var lblName = new Label() {
                X = 0,
                Y = 0,
                Text = "Name: "
            };

            var lblBarcode = new Label() {
                X = 0,
                Y = Pos.Bottom(lblName),
                Text = "Barcode: "
            };

            var lblAgeRestricted = new Label() {
                X = 0,
                Y = Pos.Bottom(lblBarcode),
                Text = "Age restricted: "
            };

            var lblCountable = new Label() {
                X = 0,
                Y = Pos.Bottom(lblAgeRestricted),
                Text = "Countable: "
            };
            
            Add(lblName, lblBarcode, lblAgeRestricted, lblCountable);
        }
        
        private void 
    }
}