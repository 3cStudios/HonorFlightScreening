# Honor Flight Veteran Screening Form

A mobile-first web application for conducting medical and mobility screenings of veterans participating in Honor Flight programs. This digital form replaces paper-based interviews and provides a streamlined, professional interface for collecting essential health and safety information.

## About Honor Flight

Honor Flight is a non-profit organization dedicated to transporting America's veterans to Washington, D.C. to visit the memorials dedicated to their service. This screening form ensures veteran safety and comfort during these meaningful journeys.

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
- Progress indicator
- Patriotic design elements

## Getting Started

### Prerequisites
- Modern web browser (Chrome, Firefox, Safari, Edge)
- No server or database required - runs entirely in the browser

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

### Deployment
The application is a single HTML file that can be deployed anywhere:
- Static hosting (Netlify, Vercel, GitHub Pages)
- Web servers (Apache, Nginx)
- Intranet systems
- Tablet kiosks

## Usage

### For Interviewers
1. Enter the veteran's name at the top of the form
2. Work through each section systematically:
   - Medical History Form Review
   - Medical Assessment
   - Mobility and Travel
   - Post Interview Review
3. Add any additional notes in the Notes section
4. Review alert flags before completing the interview

### Form Sections

#### Medical History Form Review
- Verifies if Primary Care Provider signature is present on medical forms

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

### Technologies Used
- **HTML5** - Semantic markup
- **CSS3** - Modern styling with flexbox and grid
- **Vanilla JavaScript** - No dependencies or frameworks
- **Responsive Design** - Mobile-first approach

### Browser Support
- Chrome 60+
- Firefox 60+
- Safari 12+
- Edge 79+
- Mobile browsers (iOS Safari, Chrome Mobile)

### File Structure
```
honor-flight-screening/
├── index.html              # Main application file
├── README.md              # This file
├── assets/               # Optional assets directory
│   ├── images/           # Screenshots, logos
│   └── docs/            # Additional documentation
└── examples/            # Example forms or test data
```

## 📸 Screenshots

![Honor Flight Screening Form](assets/images/form-overview.png)
*Main form interface showing medical screening section*

![Mobile View](assets/images/mobile-view.png)
*Mobile-optimized view on smartphone*

## Contributing

We welcome contributions from the community! Please follow these guidelines:

### Development Setup
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/new-feature`
3. Make your changes
4. Test thoroughly on multiple devices
5. Submit a pull request

### Code Style
- Use semantic HTML elements
- Follow BEM CSS methodology for class naming
- Write accessible code (WCAG 2.1 AA compliance)
- Comment complex JavaScript functions
- Maintain mobile-first responsive design

### Testing Checklist
- [ ] Form works on mobile devices (320px+)
- [ ] All buttons and inputs are accessible
- [ ] Conditional logic functions correctly
- [ ] Character counting works properly
- [ ] Form data can be captured/exported
- [ ] Visual design matches specifications

## 📋 Roadmap

### Planned Features
- [ ] **Data Export** - JSON/CSV export functionality
- [ ] **Print Support** - Printer-friendly form layouts
- [ ] **Offline Mode** - Service worker for offline capability
- [ ] **Multi-language** - Spanish translation support
- [ ] **Digital Signatures** - Electronic signature capture
- [ ] **Integration APIs** - EMR system integration
- [ ] **Analytics** - Form completion tracking
- [ ] **Accessibility** - Enhanced screen reader support

### Version History
- **v1.0.0** - Initial release with core screening functionality
- **v1.1.0** - Mobile responsiveness improvements
- **v1.2.0** - Conditional logic enhancements

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

### For Technical Issues
- Create an issue in this repository
- Email: tech-support@honorflight.org
- Documentation: [Wiki](https://github.com/3cstudios/honor-flight-screening/wiki)

### For Honor Flight Program Questions
- Visit: [Honor Flight Network](https://www.honorflight.org)
- Contact your local Honor Flight hub

## Acknowledgments

- Honor Flight Network for their service to veterans
- The veteran community for their feedback and input
- Contributors and maintainers of this project
- Open source community for tools and inspiration

---

**Made with ❤️ for our veterans**

*This project is dedicated to all veterans who have served our country with honor and dignity.*
