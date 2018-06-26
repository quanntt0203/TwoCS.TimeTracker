export const getSubDomain = url => {
  if (!url) {
    return "";
  }

  const matches = url.match(/^https?\:\/\/([^\/?#]+)(?:[\/?#]|$)/i);
  const domain = matches && matches[1];
  const parts = domain.split(".");
  return parts[0];
};

export const isSignedIn = () => {

    //debugger
    const token = getToken();

    if (token) {
        return true;
    }

    return false;
};

export const getToken = () => {
    const token = localStorage.getItem("token");
    return token;
};

