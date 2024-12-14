import {BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import Login from "./pages/Login";
import PageNotFound from "./pages/PageNotFound";
import Dashboard from "./pages/Dashboard";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Toaster } from "react-hot-toast";
import Signup from "./pages/Signup";
import VerifyEmail from "./pages/VerifyEmail";

const queryClient = new QueryClient({
  defaultOptions: {
      queries: {
          staleTime: 0,
      },
  },
});

function App() {
  
  return (
    <QueryClientProvider client={queryClient}>
        <BrowserRouter>
        <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<Signup />} />
          <Route path="/verify-email" element={<VerifyEmail/>} />
          <Route path="*" element={<PageNotFound />} />
        </Routes>
        </BrowserRouter>
        <Toaster position="top-right" gutter={12}
      containerStyle={{margin:"8px"}}
      toastOptions={{
        icon: false,
        success:{
          duration: 3000,
        },
        error:{
          duration: 5000,
        },
        style: {
          zIndex: 1000, 
          fontSize: '24px',
          fontWeight: 'bold',
          width: '500px', 
          padding: '24px 24px',
          background: 'linear-gradient(to right, #cdb7de ,#d4aeef ,#81688b,#6c5c7e)', 
      }}}/>

    </QueryClientProvider>
  );
}
export default App;