# ScarletSec Web Frontend

The frontend application for ScarletSec, a professional offensive security management platform.

## 🚀 Features

- **Modern UI**: Built with React, TypeScript, and Material UI
- **Dark Mode**: Enterprise-grade dark theme enabled by default
- **Authentication**: JWT-based authentication with route protection
- **Responsive Design**: Mobile-first responsive layout
- **Type Safety**: Full TypeScript coverage with strict typing
- **API Integration**: Axios-based HTTP client with interceptors

## 🛠️ Tech Stack

- **React 19** - Modern React with hooks and concurrent features
- **TypeScript** - Type-safe JavaScript
- **Vite** - Fast build tool and dev server
- **Material UI** - Enterprise-grade component library
- **React Router** - Client-side routing
- **Axios** - HTTP client with interceptors
- **ESLint + Prettier** - Code quality and formatting

## 📁 Project Structure

```
src/
├── api/              # HTTP client and API bindings
├── auth/             # Authentication context and guards
├── components/       # Reusable UI components
├── layouts/          # Application layouts
├── pages/            # Route-level pages
├── routes/           # Route definitions
├── services/         # Business logic services
├── styles/           # Theme and global styles
├── types/            # TypeScript type definitions
└── utils/            # Helper functions
```

## 🚀 Getting Started

### Prerequisites

- Node.js 18+
- npm or yarn

### Installation

1. **Install dependencies**:
   ```bash
   npm install
   ```

2. **Configure environment**:
   ```bash
   cp .env.example .env
   # Edit .env with your API base URL
   ```

3. **Start development server**:
   ```bash
   npm run dev
   ```

4. **Open your browser** to `http://localhost:5173`

### Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run lint` - Run ESLint
- `npm run lint:fix` - Fix ESLint issues
- `npm run format` - Format code with Prettier
- `npm run type-check` - Run TypeScript type checking
- `npm run preview` - Preview production build

## 🔐 Authentication

The application uses JWT-based authentication:

- **Login**: POST `/api/auth/login`
- **Token Storage**: LocalStorage (automatically managed)
- **Route Protection**: Automatic redirects for unauthenticated users
- **Tenant Support**: X-Tenant-ID header injection

## 🎨 Theme

- **Dark Mode**: Enabled by default
- **Material UI**: Custom theme with enterprise colors
- **Responsive**: Mobile-first design
- **Accessibility**: WCAG compliant components

## 🔌 API Integration

- **Base URL**: Configurable via `VITE_API_BASE_URL`
- **JWT Injection**: Automatic token header injection
- **Error Handling**: Global error interceptor
- **Tenant Headers**: Automatic tenant context

## 📱 Pages

- **Login**: Authentication page
- **Dashboard**: Overview and statistics
- **Projects**: Project management
- **Vulnerabilities**: Vulnerability tracking
- **Reports**: Report generation and history
- **Assets**: Asset inventory management

## 🏢 Enterprise Features

- **Multi-tenant**: Tenant isolation and context
- **Role-based Access**: Hierarchical permission system
- **Audit Trail**: Ready for activity logging
- **Scalable Architecture**: Clean separation of concerns

## 🤝 Contributing

1. Follow the existing code style
2. Run `npm run lint` and `npm run type-check` before committing
3. Use meaningful commit messages
4. Test your changes thoroughly

## 📄 License

This project is part of the ScarletSec platform. See the main project for licensing information.

The React Compiler is not enabled on this template because of its impact on dev & build performances. To add it, see [this documentation](https://react.dev/learn/react-compiler/installation).

## Expanding the ESLint configuration

If you are developing a production application, we recommend updating the configuration to enable type-aware lint rules:

```js
export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...

      // Remove tseslint.configs.recommended and replace with this
      tseslint.configs.recommendedTypeChecked,
      // Alternatively, use this for stricter rules
      tseslint.configs.strictTypeChecked,
      // Optionally, add this for stylistic rules
      tseslint.configs.stylisticTypeChecked,

      // Other configs...
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```

You can also install [eslint-plugin-react-x](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-x) and [eslint-plugin-react-dom](https://github.com/Rel1cx/eslint-react/tree/main/packages/plugins/eslint-plugin-react-dom) for React-specific lint rules:

```js
// eslint.config.js
import reactX from 'eslint-plugin-react-x'
import reactDom from 'eslint-plugin-react-dom'

export default defineConfig([
  globalIgnores(['dist']),
  {
    files: ['**/*.{ts,tsx}'],
    extends: [
      // Other configs...
      // Enable lint rules for React
      reactX.configs['recommended-typescript'],
      // Enable lint rules for React DOM
      reactDom.configs.recommended,
    ],
    languageOptions: {
      parserOptions: {
        project: ['./tsconfig.node.json', './tsconfig.app.json'],
        tsconfigRootDir: import.meta.dirname,
      },
      // other options...
    },
  },
])
```
