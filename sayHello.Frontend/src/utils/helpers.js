import { format } from "date-fns";

export function formatTime(isoDateString) {
  if (!isoDateString) return "Invalid Date";

  let normalizedDate = isoDateString;
  if (isoDateString.match(/T\d{2}:\d{2}:\d{2}\.\d{2}$/)) {
    normalizedDate = isoDateString + "0";
  }

  const date = new Date(normalizedDate);
  if (isNaN(date)) return "Invalid Date";

  return format(date, "h:mma").toLowerCase();
}
