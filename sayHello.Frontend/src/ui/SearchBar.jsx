function SearchBar({
  placeholder = "Search",
  onSearch,
  className = "",
  style = {},
}) {
  const handleInputChange = (e) => {
    if (onSearch) {
      onSearch(e.target.value);
    }
  };

  return (
    <input
      className={`rounded-3xl m-7 p-1 pl-7 w-5/6 shadow-2xl transition-all  border-2 border-gray-300 focus:outline-blue duration-300 ${className}`}
      style={style}
      placeholder={placeholder}
      onChange={handleInputChange}
      type="text"
    />
  );
}

export default SearchBar;

