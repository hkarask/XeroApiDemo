import { Stack, Skeleton as ChSkeleton } from "@chakra-ui/react";

const Row = () => (
  <ChSkeleton height="20px" startColor="gray.100" endColor="gray.200" />
);

const Skeleton = ({
  isLoaded,
  rows = 5,
}: {
  isLoaded: boolean;
  rows?: number;
}) => {
  if (isLoaded) return null;
  return (
    <Stack>
      {[...new Array(rows).keys()].map((i) => (
        <Row key={i} />
      ))}
    </Stack>
  );
};

export default Skeleton;
