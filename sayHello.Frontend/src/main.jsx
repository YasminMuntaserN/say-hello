import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.jsx";
import "./index.css";
import { ChatProvider } from "./context/UserContext.jsx";
import { GroupProvider } from "./context/GroupContext.jsx";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 0,
    },
  },
});

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      <ChatProvider>
      <GroupProvider>
          <App />
      </GroupProvider>
      </ChatProvider>
    </QueryClientProvider>
  </StrictMode>
);
