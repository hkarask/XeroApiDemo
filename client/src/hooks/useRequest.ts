import { useEffect, useState } from "react";

export type State<T> = {
  loading: boolean;
  error: string;
  data?: T
}

const useRequest = <T>(uri: string): State<T> => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [data, setData] = useState<T>();

  useEffect(() => {
    execute();
  }, []);

  const execute = async () => {
    setLoading(true);
    const response = await fetch(uri);
    if (response.status > 400) {
      setError(`Response: ${response.status} ${response.statusText}`);
    } else {
      const model = (await response.json()) as T;
      setData(model);
    }
    setLoading(false);
  };
  return { loading, error, data };
};
export default useRequest;
