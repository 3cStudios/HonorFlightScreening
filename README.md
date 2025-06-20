# Honor Flight Veteran Screening Form

A mobile-first web application for conducting medical and mobility screenings of veterans participating in Honor Flight programs. This digital form replaces paper-based interviews and provides a streamlined, professional interface for collecting essential health and safety information.

## Features

### Core Functionality
- **Mobile-Optimized Design** - Responsive interface designed for tablets and smartphones
- **Conditional Logic** - Dynamic form fields that appear based on user responses
- **Real-time Validation** - Character limits and input validation
- **Professional UI** - Clean, accessible design matching Honor Flight branding

### Screening Categories
- **Medical History Review** - PCP signature verification
- **Medical Assessment** - Oxygen, insulin, medication screening
- **Mobility Evaluation** - Assistive device requirements and travel concerns
- **Post-Interview Review** - Alert flags for medical, mobility, and special issues
- **Notes Section** - Free-form notes with character limit

### Key Features
- Veteran name capture
- YES/NO button selections with visual feedback
- Expandable sections for detailed information
- Device selection (cane, walker, wheelchair, scooter)
- 500-character notes field with live counter

## Getting Started

### Prerequisites
- Modern web browser (Chrome, Firefox, Safari, Edge)


### Installation
1. Clone the repository:
```bash
git clone https://github.com/your-org/honor-flight-screening.git
cd honor-flight-screening
```

2. Open the form:
```bash
# Simply open the HTML file in your browser
open index.html
# or
python -m http.server 8000  # For local development server
```


#### Medical Assessment
- **Oxygen Use** - Includes amount specification
- **Insulin Use** - Self-administration and refrigeration requirements
- **Fluid Pills** - Diuretic medication usage
- **Medical Concerns** - Open-ended travel health concerns

#### Mobility and Travel
- **Assistive Devices** - Multiple selection checkboxes
- **Bus Navigation** - Step climbing concerns
- **Flying Concerns** - Air travel specific issues

#### Post Interview Review
- **Medical Issue Alert** - Flags requiring medical attention
- **Mobility Issue Alert** - Flags requiring mobility assistance
- **Special Issue Alert** - Other concerns requiring attention

## Design System

### Color Palette
- **Primary Blue**: `#4a5568` (Headers, sections)
- **Accent Red**: `#dc2626` (Selected buttons, alerts)
- **Background**: `#f5f5f5` (Page background)
- **Card Background**: `#ffffff` (Form background)
- **Input Background**: `#f8f9fa` (Form fields)

### Typography
- **Font Family**: System fonts (-apple-system, BlinkMacSystemFont, Segoe UI)
- **Header**: 18px bold
- **Body Text**: 16px medium
- **Labels**: 16px medium weight
- **Helper Text**: 14px regular

### Interactive Elements
- **Buttons**: 12px padding, rounded corners, hover states
- **Form Fields**: Consistent padding and border radius
- **Responsive Design**: Optimized for 320px+ screens

## 🔧 Technical Details

### Browser Support
- Chrome 60+
- Firefox 60+
- Safari 12+
- Edge 79+
- Mobile browsers (iOS Safari, Chrome Mobile)

### Development Setup
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/new-feature`
3. Make your changes
4. Test thoroughly on multiple devices
5. Submit a pull request


### Testing Checklist
- [ ] Form works on mobile devices (320px+)
- [ ] All buttons and inputs are accessible
- [ ] Conditional logic functions correctly
- [ ] Character counting works properly
- [ ] Form data can be captured/exported
- [ ] Visual design matches specifications

### Version History
- **v1.0.0** - Initial release with core screening functionality

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

### For Technical Issues
- Create an issue in this repository
- Email: support@3cstudios.com


**Made with ❤️ for our veterans**

*This project is dedicated to all veterans who have served our country with honor and dignity.*
